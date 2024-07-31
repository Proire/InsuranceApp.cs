using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CQRS.Commands.PaymentCommands
{
    public class InsertPaymentCommand : IRequest
    {
        public int CustomerID { get; set; }
        public int PolicyID { get; set; }
        public double Amount { get; set; }

        public InsertPaymentCommand(int customerId, int policyId, double amount)
        {
            CustomerID = customerId;
            PolicyID = policyId;
            Amount = amount;
        }
    }
}
