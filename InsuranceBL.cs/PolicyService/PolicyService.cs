using InsuranceAppRLL.CQRS.Queries.PolicyQueries;
using InsuranceAppRLL.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppBLL.PolicyService
{
    public class PolicyService : IPolicyService
    {
        private readonly IMediator _mediator;

        public PolicyService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IEnumerable<Policy>> GetAllPoliciesForCustomersAsync(int customerId)
        {
            return await _mediator.Send(new GetAllPoliciesForCustomersQuery(customerId));
        }

        public async Task<Policy> GetPolicyByIdAsync(int policyId)
        {
            return await _mediator.Send(new GetPolicyByIdQuery(policyId));
        }
    }
}
