using InsuranceAppRLL.CQRS.Commands.PolicyCommands.InsuranceAppBLL.Commands;
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
    public class UpdatePolicyCommandHandler : IRequestHandler<UpdatePolicyCommand>
    {
        private readonly IPolicyCommandRepository _policyCommandRepository;

        public UpdatePolicyCommandHandler(IPolicyCommandRepository policyCommandRepository)
        {
            _policyCommandRepository = policyCommandRepository;
        }

        public async Task<Unit> Handle(UpdatePolicyCommand request, CancellationToken cancellationToken)
        {
            var policy = new Policy
            {
                PolicyID = request.PolicyID,
                CustomerID = request.CustomerID,
                SchemeID = request.SchemeID,
                PolicyDetails = request.PolicyDetails,
                Premium = request.Premium,
                DateIssued = request.DateIssued,
                MaturityPeriod = request.MaturityPeriod,
                PolicyLapseDate = request.PolicyLapseDate
            };

            await _policyCommandRepository.UpdatePolicy(policy);
            return Unit.Value;
        }
    }
}
