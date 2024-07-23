using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.Entities
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerID { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [StringLength(15)]
        public string Phone { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; } = DateTime.MinValue;

        // Foreign key - AgentID
        public int? AgentID { get; set; }
        [ForeignKey("AgentID")]
        // Inverse Navigation Property - one customer will have only one Insurance Agent 
        public virtual InsuranceAgent InsuranceAgent { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property - one customer have many policies 
        public virtual ICollection<Policy> Policies { get; set; }

        // Navigation property - one customer makes many payments(EMI)
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
