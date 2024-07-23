using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.Entities
{
    public class InsurancePlan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlanID { get; set; }

        [Required]
        [StringLength(100)]
        public string PlanName { get; set; } = string.Empty;

        [Required]
        public string PlanDetails { get; set; } = string.Empty;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property - one Insurance plan have many schemes
        public virtual ICollection<Scheme> Schemes { get; set; }
    }
}
