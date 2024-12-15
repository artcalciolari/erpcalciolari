using ErpCalciolari.Models;

namespace ErpCalciolari.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> CreateCustomerAsync(Customer customer);
        Task<Customer> GetCustomerWithEmailAsync(string email);
        Task<Customer> GetCustomerWithPhoneAsync(string phone);
        Task<Customer> GetCustomerWithIdAsync(Guid id);
        Task<List<Customer>> GetAllCustomersAsync();
        Task<bool> UpdateCustomerAsync(Customer customer);
        Task<bool> DeleteCustomerWithIdAsync(Guid id);
    }
}
