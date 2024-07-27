using InsuranceAppRLL.CQRS.Commands.SchemeCommands;
using InsuranceAppRLL.CQRS.Queries.SchemeQueries;
using InsuranceAppRLL.Entities;
using InsuranceMLL.SchemeModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppBLL.SchemeService
{
    public class SchemeService : ISchemeService
    {
        private readonly IMediator _mediator;

        public SchemeService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IEnumerable<Scheme>> GetAllSchemesForPlanAsync(int planId)
        {
            return await _mediator.Send(new GetAllSchemesForPlanQuery(planId));
        }

        public async Task<Scheme> GetSchemeByIdAsync(int schemeId)
        {
            return await _mediator.Send(new GetSchemeByIdQuery(schemeId));
        }

        public async Task CreateSchemeAsync(SchemeRegistrationModel schemeModel)
        {
            var command = new InsertSchemeCommand(
                schemeModel.SchemeName,
                schemeModel.SchemeDetails,
                schemeModel.PlanID,
                schemeModel.SchemePrice,
                schemeModel.SchemeCover,
                schemeModel.SchemeTenure
            );
            await _mediator.Send(command);
        }

        public async Task UpdateSchemeAsync(SchemeUpdateModel schemeModel, int schemeId)
        {
            var command = new UpdateSchemeCommand(
                schemeId,
                schemeModel.SchemeName,
                schemeModel.SchemeDetails,
                schemeModel.PlanID,
                schemeModel.SchemePrice,
                schemeModel.SchemeCover,
                schemeModel.SchemeTenure
            );
            await _mediator.Send(command);
        }

        public async Task DeleteSchemeAsync(int schemeId)
        {
            await _mediator.Send(new DeleteSchemeCommand(schemeId));
        }

    }
}
