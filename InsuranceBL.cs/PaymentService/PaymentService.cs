using InsuranceAppRLL.CQRS.Commands.PaymentCommands;
using InsuranceAppRLL.CQRS.Queries.PaymentQueries;
using InsuranceAppRLL.Entities;
using InsuranceMLL.PaymentModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppBLL.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly IMediator _mediator;

        public PaymentService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task CreatePaymentAsync(PaymentCreationModel paymentModel)
        {
            var command = new InsertPaymentCommand(paymentModel.CustomerID, paymentModel.PolicyID, paymentModel.Amount);
            await _mediator.Send(command);
        }

        public async Task<IEnumerable<Payment>> GetPaymentsForPolicyAsync(int policyId)
        {
            return await _mediator.Send(new GetAllPaymentsForPolicyQuery(policyId));
        }

        public async Task<IEnumerable<Payment>> GetPaymentsForCustomerAsync(int customerId)
        {
            return await _mediator.Send(new GetAllPaymentsForCustomerQuery(customerId));
        }
    }
}
