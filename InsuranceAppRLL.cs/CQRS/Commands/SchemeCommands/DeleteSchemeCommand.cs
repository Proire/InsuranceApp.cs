using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CQRS.Commands.SchemeCommands
{
    public class DeleteSchemeCommand : IRequest
    {
        public int SchemeID { get; set; }

        public DeleteSchemeCommand(int schemeId)
        {
            SchemeID = schemeId;
        }
    }
}
