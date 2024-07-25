using InsuranceAppRLL.CQRS.Commands.PolicyCommands;
using InsuranceAppRLL.Entities;
using InsuranceAppRLL.Repositories.Interfaces.PolicyRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CQRS.Handlers.PolicyHandlers
{
    public class InsertPolicyCommandHandler : IRequestHandler<InsertPolicyCommand>
    {
        private readonly IPolicyCommandRepository _policyCommandRepository;

        public InsertPolicyCommandHandler(IPolicyCommandRepository policyCommandRepository)
        {
            _policyCommandRepository = policyCommandRepository;
        }

        public async Task<Unit> Handle(InsertPolicyCommand request, CancellationToken cancellationToken)
        {
            var policy = new Policy
            {
                CustomerID = request.CustomerID,
                SchemeID = request.SchemeID,
                PolicyDetails = request.PolicyDetails,
                Premium = request.Premium,
                DateIssued = request.DateIssued,
                MaturityPeriod = request.MaturityPeriod,
                PolicyLapseDate = request.PolicyLapseDate
            };

            await _policyCommandRepository.AddPolicy(policy);
            return Unit.Value;
        }
    }
}
