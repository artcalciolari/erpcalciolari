using ErpCalciolari.Infra;
using ErpCalciolari.Models;
using Microsoft.EntityFrameworkCore;

namespace ErpCalciolari.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MyDbContext _context;

        public ProductRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            var products = await _context.Products.ToListAsync();

            if (products == null || products.Count == 0)
            {
                throw new KeyNotFoundException("No products found.");
            }

            return products;
        }

        public async Task<Product> GetProductWithIdAsync(Guid id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id)
                ?? throw new KeyNotFoundException($"no product found with the id {id}");
            return product;
        }

        public async Task<Product> GetProductWithCodeAsync(int code)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Code == code)
                ?? throw new KeyNotFoundException($"no product found with the code {code}");
            return product;
        }

        public async Task<bool> UpdateProductAsync(Guid id, Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProductWithIdAsync(Guid id)
        {
            var product = await GetProductWithIdAsync(id);

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
