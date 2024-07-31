using InsuranceAppRLL.Entities;
using InsuranceMLL.PaymentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppBLL.PaymentService
{
    public interface IPaymentService
    {
        Task CreatePaymentAsync(PaymentCreationModel paymentModel);

        Task<IEnumerable<Payment>> GetPaymentsForPolicyAsync(int policyId);

        Task<IEnumerable<Payment>> GetPaymentsForCustomerAsync(int customerId);
    }
}
