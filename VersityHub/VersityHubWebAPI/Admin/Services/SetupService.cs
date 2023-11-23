using Microsoft.AspNetCore.Identity;
using VersityHub.VersityHubWebAPI.Customer.Model;
using VersityHub.VersityHubWebAPI.Repository;

namespace VersityHub.VersityHubWebAPI.Admin.Services
{
    public class SetupService : ISetupService
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<ApplicationCustomer> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<SetupService> _logger;
        public SetupService(
            ApplicationDBContext context,
            UserManager<ApplicationCustomer> userManager,
            RoleManager<IdentityRole> roleManger,
            ILogger<SetupService> logger)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManger;
            _logger = logger;
        }

        public async Task<string> CreateRole(string name)
        {
            //check if role exist 
            var roleExist = await _roleManager.RoleExistsAsync(name);
            if (!roleExist) //check on the role exist status
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole(name));
                //check if role has been added successfully
                if (roleResult.Succeeded)
                {
                    _logger.LogInformation($"The role {name} has been added successfully");
                    return ($"The role {name} has been added successfully");
                }
                else
                {
                    _logger.LogInformation($"The role {name} has not been added");
                    throw new ApplicationException($"The role {name} has not been added");
                }
            }
            return ($"role {name} already exist");
        }

        public async Task<string> DeleteRole(string name)
        {
            //check if role exist 
            var role = await _roleManager.FindByNameAsync(name);
            if (role != null) //check on the role exist status
            {
                var roleResult = await _roleManager.DeleteAsync(role);
                if (roleResult.Succeeded)
                {
                    _logger.LogInformation($"The role {name} has been deleted successfully");
                    return ($"The role {name} has been deleted successfully");
                }
                else
                {
                    _logger.LogInformation($"The role {name} has not been deleted");
                    throw new ApplicationException($"The role {name} has not been deleted");
                }
            }
            return ($"role {name} does not exist");
        }

        public async Task<string> AssignRole(string email, string roleName)
        {
            //check if user exist
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                _logger.LogInformation($"This user {email} does not exist");
                return ($"This user {email} does not exist. Check the email and try again.");
            }
            //check if role exist
            var roleExist = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                _logger.LogInformation($"The role {roleName} you are trying to assign does not exist");
                return ($"The role {roleName} you are trying to assign does not exist");
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);
            //check if user is assigned successfully
            if (result.Succeeded)
            {
                _logger.LogInformation($"The role {roleName} has been assigned to user {email} successfully");
                return ($"The role {roleName} has been assigned to user {email} successfully");
            }
            else
            {
                _logger.LogInformation($"User has already been assigned to this role");
                return ($"User has already been assigned to this role");
            }
        }

        public async Task<string> DeleteUserRole(string email, string roleName)
        {
            //check if user exist
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                _logger.LogInformation($"This user {email} does not exist");
                return ($"This user {email} does not exist. Check the email and try again.");
            }
            //check if role exist
            var roleExist = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                _logger.LogInformation($"The role {roleName} you are trying to delete does not exist");
                return ($"The role {roleName} you are trying to delete does not exist");
            }

            var result = await _userManager.RemoveFromRoleAsync(user, roleName);
            //check if user is assigned successfully
            if (result.Succeeded)
            {
                _logger.LogInformation($"User {email} removed from {roleName} role successfully");
                return ($"User {email} removed from {roleName} role successfully");
            }
            else
            {
                _logger.LogInformation($"user is not assigned to this role");
                return ($"user is not assigned to this role");
            }
        }

        public async Task<string> DeleteUserAccount(string email)
        {
            //check if user exist
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                _logger.LogInformation($"This user {email} does not exist");
                return ($"This user {email} does not exist. Check the email and try again.");
            }

            var result = await _userManager.DeleteAsync(user);
            //check if user is assigned successfully
            if (result.Succeeded)
            {
                _logger.LogInformation($"User {email} account deleted successfully");
                return ($"User {email} account deleted successfully");
            }
            else
            {
                _logger.LogInformation($"unable to delete account");
                return ($"unable to delete account");
            }
        }
    }
}

