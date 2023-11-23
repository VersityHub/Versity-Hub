using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VersityHub.VersityHubWebAPI.Customer.Model;


namespace VersityHub.VersityHubWebAPI.Customer.Seller
{
    public class SellerService : ISellerService
    {
        private readonly UserManager<ApplicationCustomer> _userManager;
        private readonly SignInManager<ApplicationCustomer> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<SellerService> _logger;
        public SellerService(
            UserManager<ApplicationCustomer> userManager,
            SignInManager<ApplicationCustomer> signInManager,
            IConfiguration configuration,
            ILogger<SellerService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _logger = logger;
        }
        public async Task<IdentityResult> CreatAccountAsync(ApplicationCustomer createSellerAccount)
        {
            createSellerAccount.UserName = createSellerAccount.Email;
            createSellerAccount.PhoneNumber = createSellerAccount.Number;
            await _userManager.CreateAsync(createSellerAccount, createSellerAccount.Password);
            return await _userManager.AddToRoleAsync(createSellerAccount, "Seller");
        }

        public async Task<string> LogInAsync(CustomerLogin customerLogin)
        {
            var result = await _signInManager.PasswordSignInAsync(customerLogin.Email, customerLogin.Password, false, false);
            if (!result.Succeeded)
            {
                _logger.LogInformation($"wrong {customerLogin.Email} or {customerLogin.Password}");
                return ("Incorrect Email or Password");
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
