using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceMLL.PolicyModels
{
    public class PolicyRegistrationModel
    {
        [Required(ErrorMessage = "Customer ID is required.")]
        [DefaultValue(1)]
        public int CustomerID { get; set; } = 1;

        [Required(ErrorMessage = "Scheme ID is required.")]
        [DefaultValue(1)]
        public int SchemeID { get; set; } = 1;

        [Required(ErrorMessage = "Policy Details are required.")]
        [StringLength(500, ErrorMessage = "Policy Details cannot exceed 500 characters.")]
        [DefaultValue("Comprehensive coverage for health and life.")]
        public string PolicyDetails { get; set; } = "Comprehensive coverage for health and life.";

        [Required(ErrorMessage = "Premium is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Premium must be a positive value.")]
        [DefaultValue(299.99)]
        public double Premium { get; set; } = 299.99;

        [Required(ErrorMessage = "Date Issued is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        [DefaultValue("2024-01-01")]
        public DateTime DateIssued { get; set; } = new DateTime(2024, 1, 1);

        [Required(ErrorMessage = "Maturity Period is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Maturity Period must be a positive number.")]
        [DefaultValue(10)]
        public int MaturityPeriod { get; set; } = 10;

        [Required(ErrorMessage = "Policy Lapse Date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        [DefaultValue("2034-01-01")]
        public DateTime PolicyLapseDate { get; set; } = new DateTime(2034, 1, 1);
    }
}
