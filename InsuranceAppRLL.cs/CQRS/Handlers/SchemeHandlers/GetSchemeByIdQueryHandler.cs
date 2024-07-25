using InsuranceAppRLL.CQRS.Queries.SchemeQueries;
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
    public class GetSchemeByIdQueryHandler : IRequestHandler<GetSchemeByIdQuery, Scheme>
    {
        private readonly ISchemeQueryRepository _schemeQueryRepository;

        public GetSchemeByIdQueryHandler(ISchemeQueryRepository schemeQueryRepository)
        {
            _schemeQueryRepository = schemeQueryRepository;
        }

        public async Task<Scheme> Handle(GetSchemeByIdQuery request, CancellationToken cancellationToken)
        {
            return await _schemeQueryRepository.GetScheme(request.SchemeId);
        }
    }
}
