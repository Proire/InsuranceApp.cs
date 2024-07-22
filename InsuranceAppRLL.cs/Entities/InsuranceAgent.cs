using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.Entities
{
    public class InsuranceAgent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AgentID { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property - one Agent have many customers
        public virtual ICollection<Customer> Customers { get; set; }

        // Navigation property - one Agent have many commissions types
        public virtual ICollection<Commission> Commissions { get; set; }
    }

}
