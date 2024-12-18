using ErpCalciolari.DTOs.Create;
using ErpCalciolari.DTOs.Update;
using ErpCalciolari.Services;
using Microsoft.AspNetCore.Mvc;

namespace ErpCalciolari.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _service;

        public EmployeeController(EmployeeService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeCreateDto createDto)
        {
            var createdEmployee = await _service.CreateEmployeeAsync(createDto);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = createdEmployee.Id }, createdEmployee);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _service.GetEmployeesAsync();
            return Ok(employees);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetEmployeeById(Guid id)
        {
            var employee = await _service.GetEmployeeWithIdAsync(id);
            return Ok(employee);
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetEmployeeWithEmail(string email)
        {
            var employee = await _service.GetEmployeeWithEmailAsync(email);
            return Ok(employee);
        }

        [HttpGet("username/{username}")]
        public async Task<IActionResult> GetEmployeeWithUsername(string username)
        {
            var employee = await _service.GetEmployeeWithUsernameAsync(username);
            return Ok(employee);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody] EmployeeUpdateDto updateDto)
        {
            var updatedEmployee = await _service.UpdateEmployeeAsync(id, updateDto);
            return Ok(updatedEmployee);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            await _service.DeleteEmployeeWithIdAsync(id);
            return NoContent();
        }
    }
}
