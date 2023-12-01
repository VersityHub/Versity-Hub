using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VersityHub.VersityHubWebAPI.Customer.Model;

namespace VersityHub.VersityHubWebAPI.Customer.Services
{
    public class JwtService: IJwtService
    {
        private readonly RoleManager<IdentityRole> _roleManger;
        private readonly UserManager<ApplicationCustomer> _userManger;
        private readonly ILogger<BuyerService> _logger;
        private readonly IConfiguration _configuration;

        public JwtService(
            UserManager<ApplicationCustomer> userManger,
            RoleManager<IdentityRole> roleManger,
            ILogger<BuyerService> logger,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _roleManger = roleManger;
            _userManger = userManger;
            _logger = logger;
        }
        public async Task<string> GenerateJwtToken(CustomerLogin customerLogin)
        {
            //Get user from userMaanger
            var user = await _userManger.FindByEmailAsync(customerLogin.Email);
            if(user == null)
            {
                _logger.LogInformation($"user {customerLogin.Email} not found");
                throw new ApplicationException("user not found");
            }

            //Get roles assigned to the user
            var userRoles = await _userManger.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, customerLogin.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            //Include user roles as claims
            authClaims.AddRange(userRoles.Select(role =>
            new Claim(ClaimTypes.Role, role)));

            var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Validissuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256Signature)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
