using InsuranceAppRLL.CQRS.Queries.SchemeQueries;
using InsuranceAppRLL.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppBLL.SchemeService
{
    public class SchemeService : ISchemeService
    {
        private readonly IMediator _mediator;

        public SchemeService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IEnumerable<Scheme>> GetAllSchemesForPlanAsync(int planId)
        {
            return await _mediator.Send(new GetAllSchemesForPlanQuery(planId));
        }

        public async Task<Scheme> GetSchemeByIdAsync(int schemeId)
        {
            return await _mediator.Send(new GetSchemeByIdQuery(schemeId));
        }
    }
}
