using ErpCalciolari.DTOs.Create;
using ErpCalciolari.DTOs.Update;
using ErpCalciolari.Services;
using Microsoft.AspNetCore.Mvc;

namespace ErpCalciolari.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _service;

        public CustomerController(CustomerService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerCreateDto createDto)
        {
            var createdCustomer = await _service.CreateCustomerAsync(createDto);
            return CreatedAtAction(nameof(GetCustomerById), new { id = createdCustomer.Id }, createdCustomer);
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _service.GetCustomersAsync();
            return Ok(customers);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCustomerById(Guid id)
        {
            var customer = await _service.GetCustomerWithIdAsync(id);
            return Ok(customer);
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetCustomerWithEmail(string email)
        {
            var customer = await _service.GetCustomerWithEmailAsync(email);
            return Ok(customer);
        }

        [HttpGet("phone/{phone}")]
        public async Task<IActionResult> GetCustomerWithPhone(string phone)
        {
            var customer = await _service.GetCustomerWithPhoneAsync(phone);
            return Ok(customer);
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetCustomerWithName(string name)
        {
            var customer = await _service.GetCustomerWithNameAsync(name);
            return Ok(customer);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCustomer(Guid id, [FromBody] CustomerUpdateDto updateDto)
        {
            var updatedCustomer = await _service.UpdateCustomerAsync(id, updateDto);
            return Ok(updatedCustomer);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            await _service.DeleteCustomerWithIdAsync(id);
            return NoContent();
        }
    }
}
