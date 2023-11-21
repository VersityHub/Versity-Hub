using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VersityHub.VersityHubWebAPI.Customer.Model;
using VersityHub.VersityHubWebAPI.Repository;

namespace VersityHub.VersityHubWebAPI.Admin.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SetUpController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<ApplicationCustomer> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<SetUpController> _logger;
        public SetUpController(
            ApplicationDBContext context,
            UserManager<ApplicationCustomer> userManager,
            RoleManager<IdentityRole> roleManger,
            ILogger<SetUpController> logger)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManger;
            _logger = logger;
        }

        [HttpGet("getRoles")]
        public IActionResult GeAlltRoles()
        {
            var roles = _roleManager.Roles.ToList();
            return Ok(roles);
        }


        [HttpPost("createRole")]
        public async Task<IActionResult> CreateRole(string name)
        {
            //check if role exist 
            var roleExist = await _roleManager.RoleExistsAsync(name);
            if (!roleExist) //check on the role exxist status
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole(name));
                //check if role has been added successfully
                if (roleResult.Succeeded)
                {
                    _logger.LogInformation($"The role {name} has been added successfully");
                    return Ok(new
                    {
                        result = $"The role {name} has been added successfully",
                        message = $"you are welcome"
                    });
                }
                else
                {
                    _logger.LogInformation($"The role {name} has not been added");
                    return BadRequest(new
                    {
                        error = $"The role {name} has not been added"
                    });
                }
            }

            return BadRequest(new { error = "Role already exist", message = $"Use a new name" });
        }

        [HttpGet("getAllUsers")]
        public async Task<IActionResult> GetAlltUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return Ok(users);
        }

        [HttpPost("assignRole")]
        public async Task<IActionResult> AssignRole(string email, string roleName)
        {
            //check if user exist
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                _logger.LogInformation($"This user {email} does not exist");
                return BadRequest(new
                {
                    error = $"This user {email} does not exist. Check the email and try again."
                });
            }
            //check if role exist
            var roleExist = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                _logger.LogInformation($"The role {roleName} you are trying to assign does not exist");
                return BadRequest(new
                {
                    error = $"The role {roleName} you are trying to assign does not exist"
                });
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);
            //check if user is assigned successfully
            if (result.Succeeded)
            {
                _logger.LogInformation($"The role {roleName} has been assigned to user {email} successfully");
                return Ok(new
                {
                    result = $"The role {roleName} has been assigned to user {email} successfully"
                });
            }
            else
            {
                _logger.LogInformation($"Unable to assign user to role");
                return BadRequest(new
                {
                    error = $"Unable to assign user to role"
                });
            }
        }

        [HttpGet("getUserRole")]
        public async Task<IActionResult> GetUserRole(string email)
        {
            //check if email is valid 
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                _logger.LogInformation($"This user {email} does not exist");
                return BadRequest(new
                {
                    error = $"This user {email} does not exist. Check the email and try again."
                });
            }
            var role = await _userManager.GetRolesAsync(user);
            return Ok(role);
        }

        [HttpPost("deleteUserRole")]
        public async Task<IActionResult> DeleteUserRole(string email, string roleName)
        {
            //check if user exist
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                _logger.LogInformation($"This user {email} does not exist");
                return BadRequest(new
                {
                    error = $"This user {email} does not exist. Check the email and try again."
                });
            }
            //check if role exist
            var roleExist = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                _logger.LogInformation($"The role {roleName} you are trying to delete does not exist");
                return BadRequest(new
                {
                    error = $"The role {roleName} you are trying to delete does not exist"
                });
            }

            var result = await _userManager.RemoveFromRoleAsync(user, roleName);
            //check if user is assigned successfully
            if (result.Succeeded)
            {
                _logger.LogInformation($"User {email} removed from {roleName} role successfully");
                return Ok(new
                {
                    result = $"User {email} removed from {roleName} role successfully"
                });
            }
            else
            {
                _logger.LogInformation($"Unable to delete user  role");
                return BadRequest(new
                {
                    error = $"Unable to remove user from role"
                });
            }
        }
    }
}
