using InsuranceAppRLL.CQRS.Queries.PolicyQueries;
using InsuranceAppRLL.Entities;
using InsuranceAppRLL.Repositories.Interfaces.PolicyRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CQRS.Handlers.PolicyHandlers
{
    public class GetAllPoliciesQueryHandler : IRequestHandler<GetAllPoliciesForCustomersQuery, IEnumerable<Policy>>
    {
        private readonly IPolicyQueryRepository _policyQueryRepository;

        public GetAllPoliciesQueryHandler(IPolicyQueryRepository policyQueryRepository)
        {
            _policyQueryRepository = policyQueryRepository;
        }

        public async Task<IEnumerable<Policy>> Handle(GetAllPoliciesForCustomersQuery request, CancellationToken cancellationToken)
        {
            return await _policyQueryRepository.GetAllPoliciesForCustomer(request.CustomerId);
        }
    }
}
