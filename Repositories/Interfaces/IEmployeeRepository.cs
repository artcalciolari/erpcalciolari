using ErpCalciolari.Models;

namespace ErpCalciolari.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee> CreateEmployeeAsync(Employee employee);
        Task<Employee> GetEmployeeWithEmailAsync(string email);
        Task<Employee> GetEmployeeWithUsernameAsync(string username);
        Task<Employee> GetEmployeeWithIdAsync(Guid id);
        Task<List<Employee>> GetAllEmployeesAsync();
        Task<bool> UpdateEmployeeAsync(Employee employee);
        Task<bool> DeleteEmployeeWithIdAsync(Guid id);
    }
}
