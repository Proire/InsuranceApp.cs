using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.Entities
{
    public class Policy
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PolicyID { get; set; }

        [Required]
        public int CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        // Inverse Navigation - one policy have only one customer 
        public virtual Customer Customer { get; set; }

        [Required]
        public int SchemeID { get; set; }
        [ForeignKey("SchemeID")]
        // Inverse Navigation property - one policy belongs to one scheme 
        public virtual Scheme Scheme { get; set; }

        [Required]
        public string PolicyDetails { get; set; } = string.Empty;

        [Required]
        public double Premium { get; set; }

        [Required]
        public DateTime DateIssued { get; set; }

        [Required]
        public int MaturityPeriod { get; set; }

        [Required]
        public DateTime PolicyLapseDate { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property - one policy have many commissions
        public virtual ICollection<Commission> Commissions { get; set; }

        // Navigation property - one policy have many payments
        public virtual ICollection<Payment> Payments { get; set; }
    }

}
