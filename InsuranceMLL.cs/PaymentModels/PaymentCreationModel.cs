using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceMLL.PaymentModels
{
    public class PaymentCreationModel
    {
        [Required]
        public int CustomerID { get; set; }

        [Required]
        public int PolicyID { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public double Amount { get; set; }
    }
}
