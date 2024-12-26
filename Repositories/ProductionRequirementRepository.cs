using ErpCalciolari.Infra;
using ErpCalciolari.Models;
using ErpCalciolari.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ErpCalciolari.Repositories
{
    public class ProductionRequirementRepository : IProductionRequirementRepository
    {
        private readonly MyDbContext _context;

        public ProductionRequirementRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task CreateProductionRequirementAsync(ProductionRequirement productionRequirement)
        {
            await _context.ProductionRequirements.AddAsync(productionRequirement);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ProductionRequirement>> GetAllProductionRequirementsAsync()
        {
            var productionRequirements = await _context.ProductionRequirements.ToListAsync();

            if (productionRequirements == null || productionRequirements.Count == 0)
            {
                throw new KeyNotFoundException("No production requirements found.");
            }
            return productionRequirements;
        }

        public async Task<ProductionRequirement> GetProductionRequirementWithProductCodeAsync(int productCode)
        {
            return await _context.ProductionRequirements.FirstOrDefaultAsync(e => e.ProductCode == productCode);
        }

        public async Task UpdateProductionRequirementAsync(ProductionRequirement productionRequirement)
        {
            _context.Entry(productionRequirement).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRequirementAsync(Guid id)
        {
            var productionRequirement = await _context.ProductionRequirements.FindAsync(id);
            if (productionRequirement != null)
            {
                _context.ProductionRequirements.Remove(productionRequirement);
                await _context.SaveChangesAsync();
            }
        }
    }
}
