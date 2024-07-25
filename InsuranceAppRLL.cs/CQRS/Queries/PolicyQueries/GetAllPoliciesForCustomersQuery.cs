using InsuranceAppRLL.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CQRS.Queries.PolicyQueries
{
    public class GetAllPoliciesForCustomersQuery : IRequest<IEnumerable<Policy>>
    {
        public int CustomerId { get; }

        public GetAllPoliciesForCustomersQuery(int customerId)
        {
            CustomerId = customerId;
        }
    }
}
