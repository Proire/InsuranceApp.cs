using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CQRS.Commands.PolicyCommands
{
    using MediatR;
    using System;

    namespace InsuranceAppBLL.Commands
    {
        public class UpdatePolicyCommand : IRequest
        {
            public int PolicyID { get; set; }
            public int CustomerID { get; set; }
            public int SchemeID { get; set; }
            public string PolicyDetails { get; set; }
            public double Premium { get; set; }
            public DateTime DateIssued { get; set; }
            public int MaturityPeriod { get; set; }
            public DateTime PolicyLapseDate { get; set; }

            public UpdatePolicyCommand(int policyId, int customerId, int schemeId, string policyDetails, double premium, DateTime dateIssued, int maturityPeriod, DateTime policyLapseDate)
            {
                PolicyID = policyId;
                CustomerID = customerId;
                SchemeID = schemeId;
                PolicyDetails = policyDetails;
                Premium = premium;
                DateIssued = dateIssued;
                MaturityPeriod = maturityPeriod;
                PolicyLapseDate = policyLapseDate;
            }
        }
    }

}
