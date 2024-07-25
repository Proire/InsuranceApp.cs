using InsuranceAppRLL.CQRS.Commands.PolicyCommands.InsuranceAppBLL.Commands;
using InsuranceAppRLL.CQRS.Commands.PolicyCommands;
using InsuranceAppRLL.CQRS.Queries.PolicyQueries;
using InsuranceAppRLL.Entities;
using InsuranceMLL.PolicyModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppBLL.PolicyService
{
    public class PolicyService : IPolicyService
    {
        private readonly IMediator _mediator;

        public PolicyService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IEnumerable<Policy>> GetAllPoliciesForCustomersAsync(int customerId)
        {
            return await _mediator.Send(new GetAllPoliciesForCustomersQuery(customerId));
        }

        public async Task<Policy> GetPolicyByIdAsync(int policyId)
        {
            return await _mediator.Send(new GetPolicyByIdQuery(policyId));
        }
        public async Task CreatePolicyAsync(PolicyRegistrationModel policyModel)
        {
            var command = new InsertPolicyCommand
            (
               policyModel.CustomerID,
               policyModel.SchemeID,
               policyModel.PolicyDetails,
               policyModel.Premium,
               policyModel.DateIssued,
               policyModel.MaturityPeriod,
               policyModel.PolicyLapseDate
            );

            await _mediator.Send(command);
        }

        public async Task UpdatePolicyAsync(PolicyUpdateModel updatePolicyModel, int policyId)
        {
            var command = new UpdatePolicyCommand(
                policyId,
                updatePolicyModel.CustomerID,
                updatePolicyModel.SchemeID,
                updatePolicyModel.PolicyDetails,
                updatePolicyModel.Premium,
                updatePolicyModel.DateIssued,
                updatePolicyModel.MaturityPeriod,
                updatePolicyModel.PolicyLapseDate
            );

            await _mediator.Send(command);
        }

        public async Task DeletePolicyAsync(int policyId)
        {
            var command = new DeletePolicyCommand(policyId);
           
            await _mediator.Send(command);
        }
    }
}
