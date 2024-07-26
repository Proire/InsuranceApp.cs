using InsuranceAppRLL.CQRS.Commands.SchemeCommands;
using InsuranceAppRLL.Entities;
using InsuranceAppRLL.Repositories.Interfaces.SchemeRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CQRS.Handlers.SchemeHandlers
{
    public class InsertSchemeCommandHandler : IRequestHandler<InsertSchemeCommand>
    {
        private readonly ISchemeCommandRepository _schemeCommandRepository;

        public InsertSchemeCommandHandler(ISchemeCommandRepository schemeCommandRepository)
        {
            _schemeCommandRepository = schemeCommandRepository;
        }

        public async Task<Unit> Handle(InsertSchemeCommand request, CancellationToken cancellationToken)
        {
            var scheme = new Scheme
            {
                SchemeName = request.SchemeName,
                SchemeDetails = request.SchemeDetails,
                PlanID = request.PlanID,
                SchemeCover = request.SchemeCover,
                SchemeTenure = request.SchemeTenure,
                SchemePrice = request.SchemePrice,  
            };

            await _schemeCommandRepository.AddScheme(scheme);
            return Unit.Value;
        }
    }
}
