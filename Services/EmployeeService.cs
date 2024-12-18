using ErpCalciolari.DTOs.Read;
using ErpCalciolari.DTOs.Create;
using ErpCalciolari.DTOs.Update;
using ErpCalciolari.Models;
using ErpCalciolari.Repositories.Interfaces;

namespace ErpCalciolari.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _repository = employeeRepository;
        }


        public async Task<Employee> CreateEmployeeAsync(EmployeeCreateDto createDto)
        {
                var employeeFromDto = new Employee(createDto.Name, createDto.Username, createDto.Email, createDto.Password);
                var createdEmployee = await _repository.CreateEmployeeAsync(employeeFromDto);
                return createdEmployee;
        }

        public async Task<List<EmployeeReadDto>> GetEmployeesAsync()
        {
                var employees = await _repository.GetAllEmployeesAsync();
                return employees.Select(e => new EmployeeReadDto
                {
                    Name = e.Name,
                    Username = e.Username,
                    Email = e.Email
                }).ToList();
        }

        public async Task<EmployeeReadDto> GetEmployeeWithEmailAsync(string email)
        {
            var employee = await _repository.GetEmployeeWithEmailAsync(email);
            return new EmployeeReadDto
            {
                Name = employee.Name,
                Username = employee.Username,
                Email = employee.Email
            };
        }

        public async Task<EmployeeReadDto> GetEmployeeWithUsernameAsync(string username)
        {
            var employee = await _repository.GetEmployeeWithUsernameAsync(username);
            return new EmployeeReadDto
            {
                Name = employee.Name,
                Username = employee.Username,
                Email = employee.Email
            };
        }

        public async Task<EmployeeReadDto> GetEmployeeWithIdAsync(Guid id)
        {
            var employee = await _repository.GetEmployeeWithIdAsync(id);
            return new EmployeeReadDto
            {
                Name = employee.Name,
                Username = employee.Username,
                Email = employee.Email
            };
        }

        public async Task<bool> UpdateEmployeeAsync(Guid id, EmployeeUpdateDto updateDto)
        {
            var employee = await _repository.GetEmployeeWithIdAsync(id);

            if (!string.IsNullOrEmpty(updateDto.Name))
                employee.Name = updateDto.Name;
            if (!string.IsNullOrEmpty(updateDto.Username))
                employee.Username = updateDto.Username;
            if (!string.IsNullOrEmpty(updateDto.Email))
                employee.Email = updateDto.Email;
            if (!string.IsNullOrEmpty(updateDto.Password))
                employee.SetPassword(updateDto.Password); // Hash the password

            return await _repository.UpdateEmployeeAsync(employee);
        }

        public async Task<bool> DeleteEmployeeWithIdAsync(Guid id)
        {
            return await _repository.DeleteEmployeeWithIdAsync(id);
        }
    }
}
