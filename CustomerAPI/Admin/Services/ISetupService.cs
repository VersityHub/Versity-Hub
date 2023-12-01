using Microsoft.AspNetCore.Mvc;

namespace VersityHub.VersityHubWebAPI.Admin.Services
{
    public interface ISetupService
    {
        
        Task<string> CreateRole(string name);
        Task<string> DeleteRole(string name);
        Task<string> AssignRole(string email, string roleName);
        Task<string> DeleteUserRole(string email, string roleName);
        Task<string> DeleteUserAccount(string email);


    }
}
