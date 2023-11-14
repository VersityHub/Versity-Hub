using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VersityHub.VersityHubWebAPI.Customer.Model;
using VersityHub.VersityHubWebAPI.Customer.Services;

namespace VersityHub.VersityHubWebAPI.Customer.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyerController : ControllerBase
    {
        private readonly IBuyerService _buyerService;

        public BuyerController(IBuyerService buyerService)
        {
            _buyerService = buyerService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody]ApplicationCustomer createBuyerAccount)
        {
            var result = await _buyerService.CreatAccountAsync(createBuyerAccount);
            return Ok("Account Created Successfully");
        }
    }
    
}
