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
    public class GetInsuranceAgentByIdQueryHandler : IRequestHandler<GetInsuranceAgentByIdQuery, InsuranceAgent>
    {
        private readonly IInsuranceAgentQueryRepository _insuranceAgentQueryRepository;

        public GetInsuranceAgentByIdQueryHandler(IInsuranceAgentQueryRepository insuranceAgentQueryRepository)
        {
            _insuranceAgentQueryRepository = insuranceAgentQueryRepository;
        }

        public async Task<InsuranceAgent> Handle(GetInsuranceAgentByIdQuery request, CancellationToken cancellationToken)
        {
            return await _insuranceAgentQueryRepository.GetAgentByIDAsync(request.AgentId);
        }
    }
}
