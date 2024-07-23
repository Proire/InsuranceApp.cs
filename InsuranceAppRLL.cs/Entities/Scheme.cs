using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.Entities
{
    public class Scheme
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SchemeID { get; set; }

        [Required]
        [StringLength(100)]
        public string SchemeName { get; set; } = string.Empty;

        [Required]
        public string SchemeDetails { get; set; } = string.Empty;

        // foreign key - Plan ID
        [Required]
        public int PlanID { get; set; }
        [ForeignKey("PlanID")]
        // Inverse Navigation - one scheme belongs to only one Insurance plan
        public virtual InsurancePlan InsurancePlan { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property - one Scheme have many policies 
        public virtual ICollection<Policy> Policies { get; set; }
        
        // Navigation property - one Scheme managed by many employees
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
