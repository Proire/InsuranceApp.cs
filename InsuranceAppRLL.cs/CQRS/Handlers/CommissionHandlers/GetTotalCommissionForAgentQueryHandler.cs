using InsuranceAppRLL.CQRS.Queries.CommissionQueries;
using InsuranceAppRLL.Repositories.Interfaces.CommissionRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CQRS.Handlers.CommissionHandlers
{
    public class GetTotalCommissionForAgentQueryHandler : IRequestHandler<GetTotalCommissionForAgentQuery, double>
    {
        private readonly ICommissionQueryRepository _commissionQueryRepository;

        public GetTotalCommissionForAgentQueryHandler(ICommissionQueryRepository commissionQueryRepository)
        {
            _commissionQueryRepository = commissionQueryRepository;
        }

        public async Task<double> Handle(GetTotalCommissionForAgentQuery request, CancellationToken cancellationToken)
        {
            return await _commissionQueryRepository.GetTotalCommissionForAgentAsync(request.AgentId);
        }
    }
}
