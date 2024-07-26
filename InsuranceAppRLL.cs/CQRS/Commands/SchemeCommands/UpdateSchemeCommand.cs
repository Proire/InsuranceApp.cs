using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CQRS.Commands.SchemeCommands
{
    public class UpdateSchemeCommand : IRequest
    {
        public int SchemeID { get; set; }
        public string SchemeName { get; set; }
        public string SchemeDetails { get; set; }
        public int PlanID { get; set; }

        [Required]
        public double SchemePrice { get; set; }

        [Required]
        public double SchemeCover { get; set; }

        [Required]
        public int SchemeTenure { get; set; }

        public UpdateSchemeCommand(int schemeId, string schemeName, string schemeDetails, int planId, double schemePrice, double schemeCover, int schemeTenure)
        {
            SchemeID = schemeId;
            SchemeName = schemeName;
            SchemeDetails = schemeDetails;
            PlanID = planId;
            SchemePrice = schemePrice;
            SchemeCover = schemeCover;
            SchemeTenure = schemeTenure;
        }
    }
}
