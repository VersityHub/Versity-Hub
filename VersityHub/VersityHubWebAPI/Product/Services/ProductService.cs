using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VersityHub.VersityHubWebAPI.Product.Model;
using VersityHub.VersityHubWebAPI.Repository;

namespace VersityHub.VersityHubWebAPI.Product.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public ProductService(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
       
        public async Task<List<ProductModel>> GetProductsAsync()
        {
            var records = await _context.Products.ToListAsync();
            return _mapper.Map<List<ProductModel>>(records);
        }

        public async Task<ProductModel> GetProductByIdAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            return _mapper.Map<ProductModel>(product);
        }

        public async Task<int> AddProductAsync(ProductModel productModel)
        {
            var product = new Products()
            {
                Title = productModel.Title,
                Category = productModel.Category,
                Description = productModel.Description,
                Price = productModel.Price
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product.Id;
        }

        public async Task UpdateProductAsync(int productId, ProductModel productModel)
        {
            var product = new Products()
            {
                Id = productId,
                Title = productModel.Title,
                Category = productModel.Category,
                Description = productModel.Description
            };
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteProductAsync(int productId)
        {
            var product = new Products() { Id = productId };
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        
    }
}
