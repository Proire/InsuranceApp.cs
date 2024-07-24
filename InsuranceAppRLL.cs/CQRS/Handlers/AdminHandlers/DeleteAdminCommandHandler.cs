using InsuranceAppRLL.CQRS.Commands.AdminCommands;
using InsuranceAppRLL.Repositories.Interfaces.AdminRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CQRS.Handlers.AdminHandlers
{
    public class DeleteAdminCommandHandler : IRequestHandler<DeleteAdminCommand>
    {
        private readonly IAdminCommandRepository _adminCommandRepository;

        public DeleteAdminCommandHandler(IAdminCommandRepository adminCommandRepository)
        {
            _adminCommandRepository = adminCommandRepository;
        }

        public async Task<Unit> Handle(DeleteAdminCommand request, CancellationToken cancellationToken)
        {
            await _adminCommandRepository.DeleteAdminAsync(request.AdminId);
            return Unit.Value;
            
        }
    }
}
