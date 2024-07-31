using InsuranceAppRLL.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CQRS.Queries.PaymentQueries
{
    public class GetAllPaymentsForPolicyQuery : IRequest<List<Payment>>
    {
        public int PolicyId { get; set; }

        public GetAllPaymentsForPolicyQuery(int policyId)
        {
            PolicyId = policyId;
        }
    }
}
