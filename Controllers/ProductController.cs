using ErpCalciolari.DTOs.Create;
using ErpCalciolari.DTOs.Update;
using ErpCalciolari.Services;
using Microsoft.AspNetCore.Mvc;

namespace ErpCalciolari.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _service;

        public ProductController(ProductService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] ProductCreateDto createDto)
        {
            var createdProduct = await _service.CreateProductAsync(createDto);
            return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);

        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _service.GetProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var product = await _service.GetProductWithIdAsync(id);
            return Ok(product);
        }

        [HttpGet("code/{code}")]
        public async Task<IActionResult> GetProductWithCode(int code)
        {
            var product = await _service.GetProductWithCodeAsync(code);
            return Ok(product);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] ProductUpdateDto updateDto)
        {
            var updatedProduct = await _service.UpdateProductAsync(id, updateDto);
            return Ok(updatedProduct);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            await _service.DeleteProductWithIdAsync(id);
            return NoContent();
        }
    }
}
