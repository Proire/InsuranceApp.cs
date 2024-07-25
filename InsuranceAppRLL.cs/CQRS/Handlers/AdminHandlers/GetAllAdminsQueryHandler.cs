using InsuranceAppRLL.CQRS.Queries.AdminQueries;
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
    public class GetAllAdminsQueryHandler : IRequestHandler<GetAllAdminsQuery, IEnumerable<Admin>>
    {
        private readonly IAdminQueryRepository _adminQueryRepository;

        public GetAllAdminsQueryHandler(IAdminQueryRepository adminQueryRepository)
        {
            _adminQueryRepository = adminQueryRepository;   
        }

        public async Task<IEnumerable<Admin>> Handle(GetAllAdminsQuery request, CancellationToken cancellationToken)
        {
            return await _adminQueryRepository.GetAllAdminsAsync();
        }
    }
}
