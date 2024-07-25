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
    public class GetAllInsurancePlansQueryHandler : IRequestHandler<GetAllInsurancePlansQuery, IEnumerable<InsurancePlan>>
    {
        private readonly IInsurancePlanQueryRepository _insurancePlanQueryRepository;

        public GetAllInsurancePlansQueryHandler(IInsurancePlanQueryRepository insurancePlanQueryRepository)
        {
            _insurancePlanQueryRepository = insurancePlanQueryRepository;
        }

        public async Task<IEnumerable<InsurancePlan>> Handle(GetAllInsurancePlansQuery request, CancellationToken cancellationToken)
        {
            return await _insurancePlanQueryRepository.GetAllInsurancePlans();
        }
    }
}
