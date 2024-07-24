using InsuranceAppRLL.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CQRS.Queries.CustomerQueries
{
    public class GetCustomerByAgentIdQuery : IRequest<IEnumerable<Customer>>
    {
        public int AgentId { get; }

        public GetCustomerByAgentIdQuery(int agentId)
        {
            AgentId = agentId;
        }
    }
}
