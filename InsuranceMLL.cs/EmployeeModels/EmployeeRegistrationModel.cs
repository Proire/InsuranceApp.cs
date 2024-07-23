using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceMLL.EmployeeModels
{
    public class EmployeeRegistrationModel
    {
        [Required(ErrorMessage = "Full Name is required.")]
        [RegularExpression(@"^[A-Z][a-zA-Z\s]*$", ErrorMessage = "Full Name must start with an uppercase letter and contain only letters and spaces.")]
        [DefaultValue("John Doe")]
        public string FullName { get; set; } = "John Doe";

        [Required(ErrorMessage = "Username is required.")]
        [RegularExpression(@"^[a-zA-Z0-9_]{5,20}$", ErrorMessage = "Username must be 5-20 characters long and contain only letters, numbers, or underscores.")]
        [DefaultValue("employeeuser")]
        public string Username { get; set; } = "employeeuser";

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [DefaultValue("employee@example.com")]
        public string Email { get; set; } = "employee@example.com";

        [Required(ErrorMessage = "Password is required.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must be at least 8 characters long, contain at least one uppercase letter, one number, and one special character.")]
        [DefaultValue("EmployeePass123!")]
        public string Password { get; set; } = "EmployeePass123!";
    }
}
