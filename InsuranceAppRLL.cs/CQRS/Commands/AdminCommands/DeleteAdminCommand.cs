using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CQRS.Commands.AdminCommands
{
    public class DeleteAdminCommand : IRequest
    {
        public int AdminId { get; set; }    

        public DeleteAdminCommand(int adminId)
        {
            AdminId = adminId;
        }   
    }
}
