using Microsoft.AspNetCore.Identity;
using VersityHub.VersityHubWebAPI.Customer.Model;

namespace VersityHub.VersityHubWebAPI.Customer.Seller
{
    public interface ISellerService
    {
        Task<IdentityResult> CreatAccountAsync(ApplicationCustomer createSellerAccount);
    }
}
