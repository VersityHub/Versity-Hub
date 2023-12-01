﻿using Microsoft.AspNetCore.Http;
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

        [HttpPost("createbuyeraccount")]
        public async Task<IActionResult> CreateAccount([FromBody]ApplicationCustomer createBuyerAccount)
        {
            var result = await _buyerService.CreateAccountAsync(createBuyerAccount);
            if (result.Succeeded)
            {
                return Ok("Account Created Successfully");
            }
            return Unauthorized("Email already has an account created");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] CustomerLogin customerLogin)
        {
            var result = await _buyerService.LogInAsync(customerLogin);
            if (string.IsNullOrEmpty(result))
            {
                return Unauthorized();
            }
            return Ok(result);
        }
    }
    
}
