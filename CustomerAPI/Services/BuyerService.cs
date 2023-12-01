using Microsoft.AspNetCore.Identity;
using VersityHub.VersityHubWebAPI.Customer.Model;

namespace VersityHub.VersityHubWebAPI.Customer.Services
{
    public class BuyerService : IBuyerService
    {
        private readonly UserManager<ApplicationCustomer> _userManager;
        private readonly SignInManager<ApplicationCustomer> _signInManager;
        private readonly IJwtService _jwtService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<BuyerService> _logger;
        public BuyerService(UserManager<ApplicationCustomer> userManager,
            SignInManager<ApplicationCustomer> signInManager,
            IJwtService jwtService,
            IConfiguration configuration,
            ILogger<BuyerService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
            _configuration = configuration;
            _logger = logger;
        }
        public async Task<IdentityResult> CreateAccountAsync(ApplicationCustomer createBuyerAccount)
        {
            createBuyerAccount.UserName = createBuyerAccount.Email;
            createBuyerAccount.PhoneNumber = createBuyerAccount.Number;
            await _userManager.CreateAsync(createBuyerAccount, createBuyerAccount.Password);
            return await _userManager.AddToRoleAsync(createBuyerAccount, "Buyer");

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

