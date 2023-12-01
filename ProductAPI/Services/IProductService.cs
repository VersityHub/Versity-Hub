using VersityHub.VersityHubWebAPI.Product.Model;

namespace VersityHub.VersityHubWebAPI.Product.Services
{
    public interface IProductService
    {
        Task<List<ProductModel>> GetProductsAsync();
        Task<ProductModel> GetProductByIdAsync(int productId);
        Task<ProductModel> GetProductByTitleAsync(string productTitle);
        Task<ProductModel> GetProductByCategoryAsync(string productCategory);
        Task<int> AddProductAsync(ProductModel productModel);
        Task UpdateProductAsync(int productId, ProductModel productModel);
        Task DeleteProductAsync(int productId);
    }
}
