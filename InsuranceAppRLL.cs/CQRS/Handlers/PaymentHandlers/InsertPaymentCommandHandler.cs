using InsuranceAppRLL.CQRS.Commands.PaymentCommands;
using InsuranceAppRLL.Entities;
using InsuranceAppRLL.Repositories.Interfaces.PaymentRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CQRS.Handlers.PaymentHandlers
{
    public class InsertPaymentCommandHandler : IRequestHandler<InsertPaymentCommand>
    {
        private readonly IPaymentCommandRepository _paymentCommandRepository;

        public InsertPaymentCommandHandler(IPaymentCommandRepository paymentCommandRepository)
        {
            _paymentCommandRepository = paymentCommandRepository;
        }

        public async Task<Unit> Handle(InsertPaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = new Payment
            {
                CustomerID = request.CustomerID,
                PolicyID = request.PolicyID,
                Amount = request.Amount,
                PaymentDate = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            };

            await _paymentCommandRepository.AddPaymentAsync(payment);
            return Unit.Value;
        }
    }
}
