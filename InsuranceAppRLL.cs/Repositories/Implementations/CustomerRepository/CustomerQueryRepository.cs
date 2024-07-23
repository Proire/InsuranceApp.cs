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

        // Method to get customers by agent ID
        public async Task<IEnumerable<Customer>> GetCustomersByAgentId(int agentId)
        {
            try
            {
                var customers = await _context.Customers.Where(c => c.AgentID == agentId).ToListAsync();

                if (!customers.Any())
                {
                    throw new CustomerException($"No agent found with id: {agentId}");
                }

                return customers;
            }
            catch (SqlException ex)
            {
                throw new CustomerException("Error occurred while fetching data: ", ex);
            }
            catch (Exception ex)
            {
                throw new CustomerException(ex.Message, ex);
            }
        }
    }
}
