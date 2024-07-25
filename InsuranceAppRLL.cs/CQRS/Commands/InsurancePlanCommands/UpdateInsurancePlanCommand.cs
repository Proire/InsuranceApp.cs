using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CQRS.Commands.InsurancePlanCommands
{
    public class UpdateInsurancePlanCommand : IRequest
    {
        public int PlanID { get; set; }

        [Required]
        [StringLength(100)]
        public string PlanName { get; set; } = string.Empty;

        [Required]
        public string PlanDetails { get; set; } = string.Empty;

        public UpdateInsurancePlanCommand(int planID, string planName, string planDetails)
        {
            PlanID = planID;
            PlanName = planName;
            PlanDetails = planDetails;
        }
    }
}
