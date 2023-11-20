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

        [HttpGet("")]
        public async Task<IActionResult> ViewAllProducts()
        {
            var products = await _productService.GetProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ViewProductById([FromRoute] int id)
        {
            var products = await _productService.GetProductByIdAsync(id);
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddNewProduct([FromBody] ProductModel productModel)
        {
            var id = await _productService.AddProductAsync(productModel);

            return CreatedAtAction(nameof(ViewProductById), new { id = id, controller = "product" }, id);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductModel productModel, [FromRoute] int id)
        {
            await _productService.UpdateProductAsync(id, productModel);

            return Ok("Product updated successfully.");

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            await _productService.DeleteProductAsync(id);

            return Ok("Product deleted successfully.");

        }
    }
}
