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
    public class UpdateSchemeCommandHandler : IRequestHandler<UpdateSchemeCommand>
    {
        private readonly ISchemeCommandRepository _schemeCommandRepository;

        public UpdateSchemeCommandHandler(ISchemeCommandRepository schemeCommandRepository)
        {
            _schemeCommandRepository = schemeCommandRepository;
        }

        public async Task<Unit> Handle(UpdateSchemeCommand request, CancellationToken cancellationToken)
        {
            var scheme = new Scheme
            {
                SchemeID = request.SchemeID,
                SchemeName = request.SchemeName,
                SchemeDetails = request.SchemeDetails,
                PlanID = request.PlanID,
                SchemeTenure = request.SchemeTenure,    
                SchemeCover = request.SchemeCover,
                SchemePrice = request.SchemePrice
            };

            await _schemeCommandRepository.UpdateScheme(scheme);
            return Unit.Value;
        }
    }
}
