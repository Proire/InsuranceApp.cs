using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceMLL.CustomerModels
{
    public class CustomerRegistrationModel
    {
        [Required(ErrorMessage = "Full Name is required.")]
        [StringLength(100, ErrorMessage = "Full Name cannot exceed 100 characters.")]
        [DefaultValue("John Doe")]
        public string FullName { get; set; } = "John Doe";

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        [DefaultValue("john.doe@example.com")]
        public string Email { get; set; } = "john.doe@example.com";

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(255, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long and cannot exceed 255 characters.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must contain at least one uppercase letter, one number, and one special character.")]
        [DefaultValue("Password123!")]
        public string Password { get; set; } = "Password123!";

        [Required(ErrorMessage = "Phone number is required.")]
        [StringLength(15, ErrorMessage = "Phone number cannot exceed 15 characters.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        [DefaultValue("123-456-7890")]
        public string Phone { get; set; } = "123-456-7890";

        [Required(ErrorMessage = "Date of Birth is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        [DefaultValue("2000-01-01")]
        public DateTime DateOfBirth { get; set; } = new DateTime(2000, 1, 1);
    }
}
