using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceMLL.InsurancePlanModels
{
    public class InsurancePlanCreationModel
    {
        [Required]
        [StringLength(100)]
        public string PlanName { get; set; } = string.Empty;

        [Required]
        public string PlanDetails { get; set; } = string.Empty;
    }
}
