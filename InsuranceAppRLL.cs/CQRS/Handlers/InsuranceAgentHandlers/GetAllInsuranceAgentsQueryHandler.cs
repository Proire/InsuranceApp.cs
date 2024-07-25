using InsuranceAppRLL.CQRS.Queries.InsuranceAgentQueries;
using InsuranceAppRLL.Entities;
using InsuranceAppRLL.Repositories.Interfaces.InsuranceAgentRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CQRS.Handlers.InsuranceAgentHandlers
{
    public class GetAllInsuranceAgentsQueryHandler : IRequestHandler<GetAllInsuranceAgentsQuery, IEnumerable<InsuranceAgent>>
    {
        private readonly IInsuranceAgentQueryRepository _insuranceAgentQueryRepository;

        public GetAllInsuranceAgentsQueryHandler(IInsuranceAgentQueryRepository insuranceAgentQueryRepository)
        {
            _insuranceAgentQueryRepository = insuranceAgentQueryRepository;
        }

        public async Task<IEnumerable<InsuranceAgent>> Handle(GetAllInsuranceAgentsQuery request, CancellationToken cancellationToken)
        {
            return await _insuranceAgentQueryRepository.GetAllAsync();
        }
    }
}
