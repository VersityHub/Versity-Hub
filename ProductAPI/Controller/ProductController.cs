using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using VersityHub.VersityHubWebAPI.Product.Model;
using VersityHub.VersityHubWebAPI.Product.Services;

namespace VersityHub.VersityHubWebAPI.Product.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [Authorize(Roles = "Seller, Buyer, Admin")]
        [HttpGet("ViewAllProducts")]
        public async Task<IActionResult> ViewAllProducts()
        {
            var products = await _productService.GetProductsAsync();
            return Ok(products);
        }

        [Authorize(Roles = "Seller, Buyer, Admin")]
        [HttpGet("ViewProductById/{id}")]
        public async Task<IActionResult> ViewProductById([FromRoute] int id)
        {
            var products = await _productService.GetProductByIdAsync(id);
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }
        
        [Authorize(Roles = "Seller, Buyer, Admin")]
        [HttpPost("ViewProductByTitle")]
        public async Task<IActionResult> ViewProductByTitle([FromBody] string productTitle)
        {
            var products = await _productService.GetProductByTitleAsync(productTitle);
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }
        
        [Authorize(Roles = "Seller, Buyer, Admin")]
        [HttpPost("ViewProductByCategory")]
        public async Task<IActionResult> ViewProductByCategory([FromBody] string productCategory)
        {
            var products = await _productService.GetProductByCategoryAsync(productCategory);
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }

        [Authorize(Roles = "Seller, Admin")]
        [HttpPost("AddNewProduct")]
        public async Task<IActionResult> AddNewProduct([FromBody] ProductModel productModel)
        {
            var id = await _productService.AddProductAsync(productModel);

            return CreatedAtAction(nameof(ViewProductById), new { id = id, controller = "product" }, id);

        }

        [Authorize(Roles = "Seller, Admin")]
        [HttpPut("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductModel productModel, [FromRoute] int id)
        {
            await _productService.UpdateProductAsync(id, productModel);

            return Ok("Product updated successfully.");

        }

        [Authorize(Roles = "Seller, Admin")]
        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            await _productService.DeleteProductAsync(id);

            return Ok("Product deleted successfully.");

        }
    }
}
