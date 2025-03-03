﻿using ErpCalciolari.DTOs.Read;
using ErpCalciolari.DTOs.Create;
using ErpCalciolari.DTOs.Update;
using ErpCalciolari.Models;
using ErpCalciolari.Repositories.Interfaces;

namespace ErpCalciolari.Services
{
    public class CustomerService
    {
        private readonly ICustomerRepository _repository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _repository = customerRepository;
        }

        public async Task<Customer> CreateCustomerAsync(CustomerCreateDto createDto)
        {
            var customerFromDto = new Customer(createDto.Name, createDto.Phone, createDto.Email, createDto.Address);
            var createdCustomer = await _repository.CreateCustomerAsync(customerFromDto);
            return createdCustomer;
        }

        public async Task<List<CustomerReadDto>> GetCustomersAsync()
        {
            var customers = await _repository.GetAllCustomersAsync();
            return customers.Select(CreateCustomerResponseDto).ToList();
        }

        public async Task<CustomerReadDto> GetCustomerWithEmailAsync(string email)
        {
            var customer = await _repository.GetCustomerWithEmailAsync(email);
            return CreateCustomerResponseDto(customer);
        }

        public async Task<CustomerReadDto> GetCustomerWithPhoneAsync(string phone)
        {
            var customer = await _repository.GetCustomerWithPhoneAsync(phone);
            return CreateCustomerResponseDto(customer);
        }

        public async Task<CustomerReadDto> GetCustomerWithIdAsync(Guid id)
        {
            var customer = await _repository.GetCustomerWithIdAsync(id);
            return CreateCustomerResponseDto(customer);
        }

        public async Task<CustomerReadDto> GetCustomerWithNameAsync(string name)
        {
            var customer = await _repository.GetCustomerWithNameAsync(name);
            return CreateCustomerResponseDto(customer);
        }

        public async Task<bool> UpdateCustomerAsync(Guid id, CustomerUpdateDto updateDto)
        {
            var customer = await _repository.GetCustomerWithIdAsync(id);

            if (!string.IsNullOrEmpty(updateDto.Name))
                customer.Name = updateDto.Name;
            if (!string.IsNullOrEmpty(updateDto.Phone))
                customer.Phone = updateDto.Phone;
            if (!string.IsNullOrEmpty(updateDto.Email))
                customer.Email = updateDto.Email;
            if (!string.IsNullOrEmpty(updateDto.Address))
                customer.Address = updateDto.Address;

            return await _repository.UpdateCustomerAsync(customer);
        }

        public async Task<bool> DeleteCustomerWithIdAsync(Guid id)
        {
            return await _repository.DeleteCustomerWithIdAsync(id);
        }

        public async Task<bool> CustomerExistsWithName(string name)
        {
            try
            {
                var customer = await _repository.GetCustomerWithNameAsync(name);
                return true;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
        }

        private CustomerReadDto CreateCustomerResponseDto(Customer customer)
        {
            return new CustomerReadDto
            {
                Name = customer.Name,
                Phone = customer.Phone,
                Email = customer.Email,
                Address = customer.Address
            };
        }
    }
}
