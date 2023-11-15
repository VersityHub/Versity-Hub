using Microsoft.AspNetCore.Identity;
using VersityHub.VersityHubWebAPI.Customer.Model;


namespace VersityHub.VersityHubWebAPI.Customer.Seller
{
    public class SellerService : ISellerService
    {
        private readonly UserManager<ApplicationCustomer> _userManager;
        public SellerService(UserManager<ApplicationCustomer> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IdentityResult> CreatAccountAsync(ApplicationCustomer createSellerAccount)
        {
            createSellerAccount.UserName = createSellerAccount.EmailAddress;
            return await _userManager.CreateAsync(createSellerAccount, createSellerAccount.Password);
        }
    }
}
