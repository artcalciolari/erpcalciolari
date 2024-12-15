using ErpCalciolari.Infra;
using ErpCalciolari.Models;
using ErpCalciolari.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ErpCalciolari.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly MyDbContext _context;

        public CustomerRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            var customers = await _context.Customers.ToListAsync();
            if (customers == null || customers.Count == 0)
            {
                throw new KeyNotFoundException("No customers found.");
            }
            return customers;
        }

        public async Task<Customer> GetCustomerWithEmailAsync(string email)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(e => e.Email == email)
                ?? throw new KeyNotFoundException($"no customer found with the email {email}");
            return customer;
        }

        public async Task<Customer> GetCustomerWithIdAsync(Guid id)
        {
            var employee = await _context.Customers.FirstOrDefaultAsync(e => e.Id == id)
                ?? throw new KeyNotFoundException($"no customer found with the id {id}");
            return employee;
        }

        public async Task<Customer> GetCustomerWithPhoneAsync(string phone)
        {
            var employee = await _context.Customers.FirstOrDefaultAsync(e => e.Phone == phone)
                ?? throw new KeyNotFoundException($"no customer found with the phone {phone}");
            return employee;
        }

        public async Task<bool> UpdateCustomerAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCustomerWithIdAsync(Guid id)
        {
            var customer = await GetCustomerWithIdAsync(id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
