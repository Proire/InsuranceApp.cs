using MediatR;
using System;
using System.Collections.Generic;
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

        public UpdateSchemeCommand(int schemeId, string schemeName, string schemeDetails, int planId)
        {
            SchemeID = schemeId;
            SchemeName = schemeName;
            SchemeDetails = schemeDetails;
            PlanID = planId;
        }
    }
}
