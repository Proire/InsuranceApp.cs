using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CQRS.Queries.CommissionQueries
{
    public class GetTotalCommissionForAgentQuery : IRequest<double>
    {
        public int AgentId { get; }

        public GetTotalCommissionForAgentQuery(int agentId)
        {
            AgentId = agentId;
        }
    }
}
