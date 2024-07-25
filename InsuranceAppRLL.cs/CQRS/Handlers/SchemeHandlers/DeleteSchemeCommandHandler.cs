using InsuranceAppRLL.CQRS.Commands.SchemeCommands;
using InsuranceAppRLL.Repositories.Interfaces.SchemeRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CQRS.Handlers.SchemeHandlers
{
    public class DeleteSchemeCommandHandler : IRequestHandler<DeleteSchemeCommand>
    {
        private readonly ISchemeCommandRepository _schemeCommandRepository;

        public DeleteSchemeCommandHandler(ISchemeCommandRepository schemeCommandRepository)
        {
            _schemeCommandRepository = schemeCommandRepository;
        }

        public async Task<Unit> Handle(DeleteSchemeCommand request, CancellationToken cancellationToken)
        {
            await _schemeCommandRepository.DeleteScheme(request.SchemeID);
            return Unit.Value;
        }
    }
}
