using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CQRS.Commands.PolicyCommands
{
    public class DeletePolicyCommand : IRequest
    {
        public int PolicyID { get; set; }

        public DeletePolicyCommand(int policyId)
        {
            PolicyID = policyId;
        }
    }
}
