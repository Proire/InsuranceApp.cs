using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceMLL.SchemeModels
{
    public class SchemeUpdateModel
    {
        [Required(ErrorMessage = "Scheme Name is required.")]
        [StringLength(100, ErrorMessage = "Scheme Name cannot exceed 100 characters.")]
        [DefaultValue("Updated Scheme Name")]
        public string SchemeName { get; set; } = "Updated Scheme Name";

        [Required(ErrorMessage = "Scheme Details are required.")]
        [StringLength(500, ErrorMessage = "Scheme Details cannot exceed 500 characters.")]
        [DefaultValue("Updated details about the scheme.")]
        public string SchemeDetails { get; set; } = "Updated details about the scheme.";

        [Required]
        public double SchemePrice { get; set; }

        [Required]
        public double SchemeCover { get; set; }

        [Required]
        public int SchemeTenure { get; set; }


        [Required(ErrorMessage = "Plan ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Plan ID must be a positive number.")]
        [DefaultValue(1)]
        public int PlanID { get; set; } = 1;
    }
}
