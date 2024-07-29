using InsuranceAppRLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.Repositories.Interfaces.PaymentRepository
{
    public interface IPaymentQueryRepository
    {
        public Task<List<Payment>> GetAllPaymentsForPolicyAsync(int policyId);
        public Task<List<Payment>> GetAllPaymentsForCustomerAsync(int customerId);
    }
}
