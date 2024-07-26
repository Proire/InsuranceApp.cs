using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InsuranceAppRLL.Entities
{
    public class EmployeeScheme
    {
        public int EmployeeSchemeID { get; set; } // Primary Key

        public int EmployeeID { get; set; }
        public int SchemeID { get; set; }

        // Navigation properties
        [JsonIgnore]
        public Employee Employee { get; set; } // Navigation property for Employee
        public Scheme Scheme { get; set; } // Navigation property for Scheme
    }

}
