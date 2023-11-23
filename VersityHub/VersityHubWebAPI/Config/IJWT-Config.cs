using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using VersityHub.VersityHubWebAPI.Customer.Model;

namespace VersityHub.VersityHubWebAPI.Config
{
    public interface IJWT_Config
    {
        Task<AuthenticateResult> GenerateJwtToken(CustomerLogin customerLogin);
    }
}
