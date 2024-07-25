using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CQRS.Commands.SchemeCommands
{
    public class InsertSchemeCommand : IRequest
    {
        public string SchemeName { get; set; }
        public string SchemeDetails { get; set; }
        public int PlanID { get; set; }

        public InsertSchemeCommand(string schemeName, string schemeDetails, int planId)
        {
            SchemeName = schemeName;
            SchemeDetails = schemeDetails;
            PlanID = planId;
        }
    }
}
