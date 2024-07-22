using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.Entities
{
    public class Commission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommissionID { get; set; }

        [Required]
        public int AgentID { get; set; }
        [ForeignKey("AgentID")]
        // Inverse Navigation property - one commission belongs to one agent only 
        public virtual InsuranceAgent InsuranceAgent { get; set; }

        [Required]
        public int PolicyID { get; set; }
        [ForeignKey("PolicyID")]
        // Inverse Navigation property - one commission belongs to one policy only 
        public virtual Policy Policy { get; set; }

        [Required]
        public double CommissionAmount { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
