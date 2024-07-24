using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceMLL.AdminModels
{
    public class AdminUpdateModel
    {
        [Required(ErrorMessage = "Full Name is required.")]
        [RegularExpression(@"^[A-Z][a-zA-Z\s]*$", ErrorMessage = "Full Name must start with an uppercase letter and contain only letters and spaces.")]
        [DefaultValue("Admin Name")]
        public string FullName { get; set; } = "Admin Name";

        [Required(ErrorMessage = "Username is required.")]
        [RegularExpression(@"^[a-zA-Z0-9_]{5,20}$", ErrorMessage = "Username must be 5-20 characters long and contain only letters, numbers, or underscores.")]
        [DefaultValue("adminuser")]
        public string Username { get; set; } = "adminuser";

        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address format.")]
        [DefaultValue("admin@example.com")]
        public string Email { get; set; } = "admin@example.com";

        [Required(ErrorMessage = "Password is required.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must be at least 8 characters long, contain at least one uppercase letter, one number, and one special character.")]
        [DefaultValue("AdminPass123!")]
        public string Password { get; set; } = "AdminPass123!";
    }
}
