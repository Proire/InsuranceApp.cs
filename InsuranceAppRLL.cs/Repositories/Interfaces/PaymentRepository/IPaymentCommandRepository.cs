using InsuranceAppRLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.Repositories.Interfaces.PaymentRepository
{
    public interface IPaymentCommandRepository
    {
        public Task AddPaymentAsync(Payment payment);
    }
}
