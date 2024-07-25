using InsuranceAppRLL.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CQRS.Queries.SchemeQueries
{
    public class GetAllSchemesForPlanQuery : IRequest<IEnumerable<Scheme>>
    {
        public int PlanId { get; }

        public GetAllSchemesForPlanQuery(int planId)
        {
            PlanId = planId;
        }
    }
}
