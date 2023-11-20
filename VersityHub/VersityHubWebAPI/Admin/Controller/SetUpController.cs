using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VersityHub.VersityHubWebAPI.Repository;

namespace VersityHub.VersityHubWebAPI.Admin.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SetUpController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<SetUpController> _logger;
        public SetUpController(
            ApplicationDBContext context,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManger,
            ILogger<SetUpController> logger)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManger;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GeAlltRoles()
        {
            var roles = _roleManager.Roles.ToList();
            return Ok(roles);
        }
    }
}
