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
            var user = new ApplicationCustomer()
            {
                FirstName = createSellerAccount.FirstName,
                MiddleName = createSellerAccount.MiddleName,
                LastName = createSellerAccount.LastName,
                Email = createSellerAccount.EmailAddress,
                PhoneNumber = createSellerAccount.PhoneNumber,
                Gender = createSellerAccount.Gender,
                State = createSellerAccount.State,
                University = createSellerAccount.University,
                UniversityID = createSellerAccount.UniversityID,
                DateOfBirth = createSellerAccount.DateOfBirth
            };

            return await _userManager.CreateAsync(user, createSellerAccount.Password);
        }
    }
}
