using ErpCalciolari.Models;

namespace ErpCalciolari.Repositories.Interfaces
{
    public interface IProductionRequirementRepository
    {
        Task<List<ProductionRequirement>> GetAllProductionRequirementsAsync();
        Task<ProductionRequirement> GetProductionRequirementWithProductCodeAsync(int productCode);
        Task CreateProductionRequirementAsync(ProductionRequirement productionRequirement);
        Task UpdateProductionRequirementAsync(ProductionRequirement productionRequirement);
        Task DeleteRequirementAsync(Guid id);
    }
}
