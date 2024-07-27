using InsuranceAppRLL.Entities;
using InsuranceMLL.CustomerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppBLL.CustomerService
{
    public interface ICustomerService
    {
        Task RegisterCustomerAsync(CustomerRegistrationModel customer,int agentId);

        Task<IEnumerable<Customer>> GetCustomers(int agentId);

        Task DeleteCustomerAsync(int customerId);

        Task UpdateCustomerAsync(CustomerUpdateModel customer, int customerId, int agentId);

        Task<IEnumerable<Customer>> GetAllCustomersAsync();

        Task<Customer> GetCustomerByIdAsync(int customerId);
    }
}
