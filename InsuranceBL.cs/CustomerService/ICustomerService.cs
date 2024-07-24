using InsuranceAppRLL.Entities;
using InsuranceMLL.CustomerModels;
using InsuranceMLL.CustomerModels.InsuranceMLL.CustomerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppBLL.CustomerService
{
    public interface ICustomerService
    {
        Task RegisterCustomerAsync(CustomerRegistrationModel customer);

        Task<IEnumerable<Customer>> GetCustomers(int agentId);
    }
}
