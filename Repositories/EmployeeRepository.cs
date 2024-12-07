using ErpCalciolari.Infra;
using ErpCalciolari.Models;
using Microsoft.EntityFrameworkCore;

namespace ErpCalciolari.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly MyDbContext _context;

        public EmployeeRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee> GetEmployeeWithEmailAsync(string email)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Email == email)
                ?? throw new KeyNotFoundException($"no user found with the email '{email}'");
            return employee;
        }

        public async Task<Employee> GetEmployeeWithUsernameAsync(string username)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Username == username)
                ?? throw new KeyNotFoundException($"no user found with the username '{username}'");
            return employee;
        }

        public async Task<Employee> GetEmployeeWithIdAsync(Guid id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id)
                ?? throw new KeyNotFoundException($"no user found with the id '{id}'");
            return employee;
        }

        public async Task<bool> UpdateEmployeeAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteEmployeeWithIdAsync(Guid id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id)
                ?? throw new Exception($"{{\"error\": \"not_found\", \"message\": \"no user found with the id '{id}'\"}}");
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
