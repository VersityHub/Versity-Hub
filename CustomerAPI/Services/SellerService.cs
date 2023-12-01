using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VersityHub.VersityHubWebAPI.Customer.Model;
using VersityHub.VersityHubWebAPI.Customer.Services;

namespace VersityHub.VersityHubWebAPI.Customer.Seller
{
    public class SellerService : ISellerService
    {
        private readonly UserManager<ApplicationCustomer> _userManager;
        private readonly SignInManager<ApplicationCustomer> _signInManager;
        private readonly IJwtService _jwtService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<SellerService> _logger;
        public SellerService(
            UserManager<ApplicationCustomer> userManager,
            SignInManager<ApplicationCustomer> signInManager,
            IJwtService jwtService,
            IConfiguration configuration,
            ILogger<SellerService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
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

            return await _jwtService.GenerateJwtToken(customerLogin);
        }
    }
}
