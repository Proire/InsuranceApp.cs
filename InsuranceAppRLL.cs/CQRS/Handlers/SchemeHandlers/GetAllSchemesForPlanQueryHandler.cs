using InsuranceAppRLL.CQRS.Queries.SchemeQueries;
using InsuranceAppRLL.Entities;
using InsuranceAppRLL.Repositories.Interfaces.SchemeRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CQRS.Handlers.SchemeHandlers
{
    public class GetAllSchemesForPlanQueryHandler : IRequestHandler<GetAllSchemesForPlanQuery, IEnumerable<Scheme>>
    {
        private readonly ISchemeQueryRepository _schemeQueryRepository;

        public GetAllSchemesForPlanQueryHandler(ISchemeQueryRepository schemeQueryRepository)
        {
            _schemeQueryRepository = schemeQueryRepository;
        }

        public async Task<IEnumerable<Scheme>> Handle(GetAllSchemesForPlanQuery request, CancellationToken cancellationToken)
        {
            return await _schemeQueryRepository.GetAllSchemasForPlan(request.PlanId);
        }
    }
}
