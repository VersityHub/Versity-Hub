using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VersityHub.VersityHubWebAPI.Customer.Model;
using VersityHub.VersityHubWebAPI.Customer.Seller;


namespace VersityHub.VersityHubWebAPI.Customer.Buyer
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly ISellerService _sellerService;
        public SellerController(ISellerService sellerService)
        {
            _sellerService = sellerService;
        }

        [HttpPost("createSellerAccount")]
        public async Task<IActionResult> CreateAccount([FromBody] ApplicationCustomer createSellerAccount)
        {
            var result = await _sellerService.CreatAccountAsync(createSellerAccount);
            if (result.Succeeded)
            {
                return Ok("Account Created Successfully");
            }
            return Unauthorized("Email already has an account created");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] CustomerLogin customerLogin)
        {
            var result = await _sellerService.LogInAsync(customerLogin);
            if (string.IsNullOrEmpty(result))
            {
                return Unauthorized();
            }
            return Ok(result);
        }
    }
}
