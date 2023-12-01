using Microsoft.AspNetCore.Identity;
using VersityHub.VersityHubWebAPI.Customer.Model;

namespace VersityHub.VersityHubWebAPI.Customer.Services
{
    public interface IBuyerService
    {
        Task<IdentityResult> CreateAccountAsync(ApplicationCustomer createBuyerAccount);
        Task<string> LogInAsync(CustomerLogin customerLogin);
    }
}
