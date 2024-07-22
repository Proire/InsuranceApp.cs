using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceAppRLL.Entities
{
    public class Admin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdminID { get; set; }

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
    }

}
