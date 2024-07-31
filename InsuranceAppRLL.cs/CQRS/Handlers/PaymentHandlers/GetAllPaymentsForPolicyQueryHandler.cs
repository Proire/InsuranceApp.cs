using InsuranceAppRLL.CQRS.Queries.PaymentQueries;
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
    public class GetAllPaymentsForPolicyQueryHandler : IRequestHandler<GetAllPaymentsForPolicyQuery, List<Payment>>
    {
        private readonly IPaymentQueryRepository _paymentQueryRepository;

        public GetAllPaymentsForPolicyQueryHandler(IPaymentQueryRepository paymentQueryRepository)
        {
            _paymentQueryRepository = paymentQueryRepository;
        }

        public async Task<List<Payment>> Handle(GetAllPaymentsForPolicyQuery request, CancellationToken cancellationToken)
        {
            return await _paymentQueryRepository.GetAllPaymentsForPolicyAsync(request.PolicyId);
        }
    }
}
