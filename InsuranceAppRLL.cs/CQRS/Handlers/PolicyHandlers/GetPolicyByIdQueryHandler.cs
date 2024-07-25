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
    public class GetPolicyByIdQueryHandler : IRequestHandler<GetPolicyByIdQuery, Policy>
    {
        private readonly IPolicyQueryRepository _policyQueryRepository;

        public GetPolicyByIdQueryHandler(IPolicyQueryRepository policyQueryRepository)
        {
            _policyQueryRepository = policyQueryRepository;
        }

        public Task<Policy> Handle(GetPolicyByIdQuery request, CancellationToken cancellationToken)
        {
            return _policyQueryRepository.GetPolicy(request.PolicyId);
        }
    }
}
