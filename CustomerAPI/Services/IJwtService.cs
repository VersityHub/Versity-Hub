using VersityHub.VersityHubWebAPI.Customer.Model;

namespace VersityHub.VersityHubWebAPI.Customer.Services
{
    public interface IJwtService
    {
        Task<string> GenerateJwtToken(CustomerLogin customerLogin);
    }
}
