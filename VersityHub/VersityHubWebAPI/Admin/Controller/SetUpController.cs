using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VersityHub.VersityHubWebAPI.Admin.Services;
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
        private readonly ISetupService _setupService;

        public SetUpController(
            ApplicationDBContext context,
            UserManager<ApplicationCustomer> userManager,
            RoleManager<IdentityRole> roleManger,
            ILogger<SetUpController> logger,
            ISetupService setupService)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManger;
            _logger = logger;
            _setupService = setupService;
        }

    
        [HttpPost("createRole")]
        public async Task<IActionResult> CreateRole(string name)
        {
            var role = await _setupService.CreateRole(name);
            return Ok(role);
        }
        
        [HttpPost("deleteRole")]
        public async Task<IActionResult> DeleteRole(string name)
        {
            var role = await _setupService.DeleteRole(name);
            return Ok(role);
        }

        [HttpGet("getRoles")]
        public IActionResult GetAllRoles()
        {
            var roles = _roleManager.Roles.ToList();
            return Ok(roles);
        }

        [HttpPost("assignRole")]
        public async Task<IActionResult> AssignRole(string email, string roleName)
        {
            var role = await _setupService.AssignRole(email, roleName);
            return Ok(role);
        }

        [HttpGet("getAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return Ok(users);
        }

        [HttpGet("getUserRole")]
        public async Task<IActionResult> GetUserRole(string email)
        {
            //check if email is valid 
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                _logger.LogInformation($"This user {email} does not exist");
                return BadRequest ($"This user {email} does not exist. Check the email and try again.");
            }
            var role = await _userManager.GetRolesAsync(user);
            return Ok(role);
        }

        [HttpPost("deleteUserRole")]
        public async Task<IActionResult> DeleteUserRole(string email, string roleName)
        {
            var deleteRole = await _setupService.DeleteUserRole(email, roleName);
            return Ok(deleteRole);
        }

        [HttpPost("deleteUserAccount")]
        public async Task<IActionResult> DeleteUserAcoount(string email)
        {
            var deleteAccount= await _setupService.DeleteUserAccount(email);
            return Ok(deleteAccount);
        }

    }
}
