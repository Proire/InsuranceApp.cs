using InsuranceAppRLL.CQRS.Commands.AdminCommands;
using InsuranceAppRLL.Entities;
using InsuranceAppRLL.Repositories.Interfaces.AdminRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CQRS.Handlers.AdminHandlers
{
    public class UpdateAdminCommandHandler : IRequestHandler<UpdateAdminCommand>
    {
        private readonly IAdminCommandRepository _adminCommandRepository;

        public UpdateAdminCommandHandler(IAdminCommandRepository adminCommandRepository)
        {
            _adminCommandRepository = adminCommandRepository;   
        }
        public async Task<Unit> Handle(UpdateAdminCommand request, CancellationToken cancellationToken)
        {
            Admin Admin = new Admin()
            {
                AdminID = request.AdminId,
                Username = request.Username,
                Email = request.Email,
                FullName = request.FullName,
                Password = request.Password
            };

            await _adminCommandRepository.UpdateAdminAsync(Admin);
            return Unit.Value;  
        }
    }
}
