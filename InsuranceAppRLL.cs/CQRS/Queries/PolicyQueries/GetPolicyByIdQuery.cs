using InsuranceAppRLL.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CQRS.Queries.PolicyQueries
{
    public class GetPolicyByIdQuery : IRequest<Policy>
    {
        public int PolicyId { get; }

        public GetPolicyByIdQuery(int policyId)
        {
            PolicyId = policyId;
        }
    }
}
