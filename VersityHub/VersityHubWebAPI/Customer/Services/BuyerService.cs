using Microsoft.AspNetCore.Identity;
using VersityHub.VersityHubWebAPI.Customer.Model;

namespace VersityHub.VersityHubWebAPI.Customer.Services
{
    public class BuyerService : IBuyerService
    {
        private readonly UserManager<ApplicationCustomer> _userManager;
        public BuyerService(UserManager<ApplicationCustomer> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IdentityResult> CreateAccountAsync(ApplicationCustomer createBuyerAccount)
        {
            createBuyerAccount.UserName = createBuyerAccount.EmailAddress;
            return await _userManager.CreateAsync(createBuyerAccount, createBuyerAccount.Password);
        }
    }
}
