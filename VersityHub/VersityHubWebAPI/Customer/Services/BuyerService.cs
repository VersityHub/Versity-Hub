using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VersityHub.VersityHubWebAPI.Customer.Model;

namespace VersityHub.VersityHubWebAPI.Customer.Services
{
    public class BuyerService : IBuyerService
    {
        private readonly UserManager<ApplicationCustomer> _userManager;
        private readonly SignInManager<ApplicationCustomer> _signInManager;
        private readonly IConfiguration _configuration;
        public BuyerService(UserManager<ApplicationCustomer> userManager,
            SignInManager<ApplicationCustomer> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }
        public async Task<IdentityResult> CreateAccountAsync(ApplicationCustomer createBuyerAccount)
        {
            createBuyerAccount.UserName = createBuyerAccount.EmailAddress;
            return await _userManager.CreateAsync(createBuyerAccount, createBuyerAccount.Password);
        }

        public async Task<string> LogInAsync(CustomerLogin customerLogin)
        {
            var result = await _signInManager.PasswordSignInAsync(customerLogin.Email, customerLogin.Password, false, false);
            if (!result.Succeeded)
            {
                return null;
            }

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

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

