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

        [HttpPost("createaccount")]
        public async Task<IActionResult> CreateAccount([FromBody] ApplicationCustomer createSellerAccount)
        {
            var result = await _sellerService.CreatAccountAsync(createSellerAccount);
            if (result.Succeeded)
            {
                return Ok("Account Created Successfully");
            }
            return Unauthorized("Failed");
        }
    }
}
