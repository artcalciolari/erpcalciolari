using ErpCalciolari.Models;

namespace ErpCalciolari.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> CreateProductAsync(Product product);
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> GetProductWithIdAsync(Guid id);
        Task<Product> GetProductWithCodeAsync(int code);
        Task<bool> UpdateProductAsync(Guid id, Product product);
        Task<bool> DeleteProductWithIdAsync(Guid id);
    }
}
