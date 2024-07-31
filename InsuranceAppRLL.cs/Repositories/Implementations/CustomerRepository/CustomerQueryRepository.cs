using InsuranceAppRLL.CustomExceptions;
using InsuranceAppRLL.Entities;
using InsuranceAppRLL.Repositories.Interfaces.CustomerRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.Repositories.Implementations.CustomerRepository
{
    public class CustomerQueryRepository : ICustomerQueryRepository
    {
        private readonly InsuranceDbContext _context;

        // Constructor to initialize the context
        public CustomerQueryRepository(InsuranceDbContext insuranceDbContext)
        {
            _context = insuranceDbContext;
        }

        public async Task<Customer> GetCustomerByIdAsync(int customerId)
        {
            try
            {
                var customer = await _context.GetCustomerByIdAsync(customerId);
                if (customer == null)
                {
                    throw new CustomerException($"Customer not found");
                }
                return customer;
            }
            catch (SqlException ex)
            {
                throw new CustomerException("An error occurred while retrieving the customer.", ex);
            }
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            try
            {
                var customers = await _context.GetAllCustomersAsync();
                if (!customers.Any())
                {
                    throw new CustomerException("No customers found.");
                }
                return customers;
            }
            catch (Exception ex)
            {
                throw new CustomerException("An error occurred while retrieving the customers.", ex);
            }
        }


        public async Task<IEnumerable<Customer>> GetCustomersByAgentIdAsync(int agentId)
        {
            try
            {
                var customers = await _context.GetCustomersByAgentIdAsync(agentId);
                if (!customers.Any())
                {
                    throw new CustomerException($"No customers found for agent ID: {agentId}");
                }
                return customers;
            }
            catch (SqlException ex)
            {
                throw new CustomerException("Error occurred while fetching data.", ex);
            }
        }
    }
}
