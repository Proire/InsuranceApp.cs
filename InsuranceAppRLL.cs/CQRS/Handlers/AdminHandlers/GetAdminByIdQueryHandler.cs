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
    public class GetAdminByIdQueryHandler : IRequestHandler<GetAdminByIdQuery, Admin>
    {
        private readonly IAdminQueryRepository _adminQueryRepository;

        public GetAdminByIdQueryHandler(IAdminQueryRepository adminQueryRepository)
        {
            _adminQueryRepository = adminQueryRepository;
        }   

        public Task<Admin> Handle(GetAdminByIdQuery request, CancellationToken cancellationToken)
        {
            return _adminQueryRepository.GetAdminByIdAsync(request.AdminId);
        }
    }
}
