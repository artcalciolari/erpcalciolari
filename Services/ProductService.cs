using ErpCalciolari.Models;
using ErpCalciolari.DTOs.Create;
using ErpCalciolari.DTOs.Read;
using ErpCalciolari.DTOs.Update;
using ErpCalciolari.Repositories.Interfaces;
namespace ErpCalciolari.Services
{
    public class ProductService
    {
        private readonly IProductRepository _repository;
        private readonly IProductionRequirementRepository _requirementRepository;

        public ProductService(IProductRepository productRepository, IProductionRequirementRepository requirementRepository)
        {
            _repository = productRepository;
            _requirementRepository = requirementRepository;
        }

        public async Task<Product> CreateProductAsync(ProductCreateDto createDto)
        {
            var productFromDto = new Product(createDto.Code, createDto.Name, createDto.Type, createDto.Quantity, createDto.Price);
            var createdProduct = await _repository.CreateProductAsync(productFromDto);
            return createdProduct;
        }

        public async Task<List<ProductReadDto>> GetProductsAsync()
        {
            var products = await _repository.GetAllProductsAsync();
            return products.Select(p => new ProductReadDto
            {
                Code = p.Code,
                Name = p.Name,
                Type = p.Type,
                Quantity = p.Quantity,
                Price = p.Price
            }).ToList();
        }

        public async Task<ProductReadDto> GetProductWithIdAsync(Guid id)
        {
            var product = await _repository.GetProductWithIdAsync(id);
            return new ProductReadDto
            {
                Code = product.Code,
                Name = product.Name,
                Type = product.Type,
                Quantity = product.Quantity,
                Price = product.Price
            };
        }

        public async Task<ProductReadDto> GetProductWithCodeAsync(int code)
        {
            var product = await _repository.GetProductWithCodeAsync(code);
            return new ProductReadDto
            {
                Code = product.Code,
                Name = product.Name,
                Type = product.Type,
                Quantity = product.Quantity,
                Price = product.Price
            };
        }

        public async Task<bool> UpdateProductAsync(Guid id, ProductUpdateDto updateDto)
        {
            var product = await _repository.GetProductWithIdAsync(id);

            if (updateDto.Quantity.HasValue && updateDto.Quantity > 0)
            {
                int newStock = product.Quantity + updateDto.Quantity.Value;

                // Recupera o requisito de produção associado ao produto
                var productionRequirement = await _requirementRepository.GetProductionRequirementWithProductCodeAsync(product.Code);

                if (productionRequirement != null)
                {
                    // Verifica se o novo estoque cobre os pedidos pendentes
                    if (newStock >= productionRequirement.RequiredQuantity)
                    {
                        // Reseta os atributos e remove o registro na tabela de produção
                        product.NeedsProduction = false;
                        await _requirementRepository.DeleteRequirementAsync(productionRequirement.Id);
                    }
                }

                product.Quantity = newStock;
            }

            if (!string.IsNullOrWhiteSpace(updateDto.Name))
                product.Name = updateDto.Name;

            if (!string.IsNullOrWhiteSpace(updateDto.Type))
                product.Type = updateDto.Type;

            if (updateDto.Price.HasValue && updateDto.Price > 0)
                product.Price = updateDto.Price.Value;

            return await _repository.UpdateProductAsync(id, product);
        }


        public async Task<bool> DeleteProductWithIdAsync(Guid id) 
        {
            return await _repository.DeleteProductWithIdAsync(id);
        }
    }
}
