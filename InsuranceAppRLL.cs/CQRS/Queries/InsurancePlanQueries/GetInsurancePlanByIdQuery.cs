using InsuranceAppRLL.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CQRS.Queries.InsurancePlanQueries
{
    public class GetInsurancePlanByIdQuery : IRequest<InsurancePlan>
    {
        public int PlanId { get; set; }

        public GetInsurancePlanByIdQuery(int planId)
        {
            PlanId = planId;
        }
    }
}
