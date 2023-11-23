using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VersityHub.VersityHubWebAPI.Customer.Model;

namespace VersityHub.VersityHubWebAPI.Config
{
    public class JWT_Config : IJWT_Config
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<JWT_Config> _logger;
        public JWT_Config(
            IConfiguration configuration,
            ILogger<JWT_Config> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<AuthenticateResult> GenerateJwtToken(CustomerLogin customerLogin)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, customerLogin.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var authSigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Validissuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256Signature)
                );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            var authenticateResult = new AuthenticateResult(jwtToken);

            return authenticateResult;
            
        }
    }
}
