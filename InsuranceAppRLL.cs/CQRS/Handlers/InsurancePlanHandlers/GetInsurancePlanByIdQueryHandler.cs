using InsuranceAppRLL.CQRS.Queries.InsurancePlanQueries;
using InsuranceAppRLL.Entities;
using InsuranceAppRLL.Repositories.Interfaces.InsurancePlanRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CQRS.Handlers.InsurancePlanHandlers
{
    public class GetInsurancePlanByIdQueryHandler : IRequestHandler<GetInsurancePlanByIdQuery, InsurancePlan>
    {
        private readonly IInsurancePlanQueryRepository _insurancePlanQueryRepository;

        public GetInsurancePlanByIdQueryHandler(IInsurancePlanQueryRepository insurancePlanQueryRepository)
        {
            _insurancePlanQueryRepository = insurancePlanQueryRepository;
        }

        public Task<InsurancePlan> Handle(GetInsurancePlanByIdQuery request, CancellationToken cancellationToken)
        {
            return _insurancePlanQueryRepository.GetInsurancePlan(request.PlanId);
        }
    }
}
