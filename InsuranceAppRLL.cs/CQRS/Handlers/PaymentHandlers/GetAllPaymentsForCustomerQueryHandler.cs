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
    public class GetAllPaymentsForCustomerQueryHandler : IRequestHandler<GetAllPaymentsForCustomerQuery, List<Payment>>
    {
        private readonly IPaymentQueryRepository _paymentQueryRepository;

        public GetAllPaymentsForCustomerQueryHandler(IPaymentQueryRepository paymentQueryRepository)
        {
            _paymentQueryRepository = paymentQueryRepository;
        }

        public async Task<List<Payment>> Handle(GetAllPaymentsForCustomerQuery request, CancellationToken cancellationToken)
        {
            return await _paymentQueryRepository.GetAllPaymentsForCustomerAsync(request.CustomerId);
        }
    }
}
