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
        public async Task<IdentityResult> CreatAccountAsync(ApplicationCustomer createBuyerAccount)
        {
            var user = new ApplicationCustomer()
            {
                FirstName = createBuyerAccount.FirstName,
                MiddleName = createBuyerAccount.MiddleName,
                LastName = createBuyerAccount.LastName,
                Email = createBuyerAccount.EmailAddress,
                PhoneNumber = createBuyerAccount.PhoneNumber,
                Gender = createBuyerAccount.Gender,
                State = createBuyerAccount.State,
                University = createBuyerAccount.University,
                UniversityID = createBuyerAccount.UniversityID
            };

            return await _userManager.CreateAsync(user, createBuyerAccount.Password);
        }
    }
}
