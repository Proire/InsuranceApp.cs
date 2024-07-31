using InsuranceAppRLL.CQRS.Queries.CommissionQueries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppBLL.CommissionService
{
    public class CommissionService : ICommissionService
    {
        private readonly IMediator _mediator;

        public CommissionService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<double> GetTotalCommissionForAgentAsync(int agentId)
        {
            var query = new GetTotalCommissionForAgentQuery(agentId);
            return await _mediator.Send(query);
        }
    }
}
