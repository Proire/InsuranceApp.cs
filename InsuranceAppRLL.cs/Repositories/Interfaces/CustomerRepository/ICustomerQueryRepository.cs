using InsuranceAppRLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.Repositories.Interfaces.CustomerRepository
{
    public interface ICustomerQueryRepository
    {
        Task<IEnumerable<Customer>> GetCustomersByAgentId(int agentId);

        Task<IEnumerable<Customer>> GetAllCustomersAsync();

        Task<Customer> GetCustomerByIdAsync(int customerId);
    }
}
