using InsuranceAppRLL.CQRS.Commands.AdminCommands;
using InsuranceAppRLL.CQRS.Queries;
using InsuranceAppRLL.CQRS.Queries.AdminQueries;
using InsuranceAppRLL.Entities;
using InsuranceMLL.AdminModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModelLayer;

namespace InsuranceAppBLL.AdminService
{
    public class AdminService : IAdminService
    {
        private readonly IMediator _mediator;

        public AdminService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task CreateAdminAsync(AdminRegistrationModel admin)
        {
            var command = new InsertAdminCommand(admin.Username,admin.Password,admin.Email,admin.FullName);
            await _mediator.Send(command);
        }

        public async Task DeleteAdminAsync(int adminId)
        {
            await _mediator.Send(new DeleteAdminCommand(adminId));
        }

        public async Task<Admin> GetAdminByIdAsync(int adminId)
        {
            return await _mediator.Send(new GetAdminByIdQuery(adminId));
        }

        public async Task<IEnumerable<Admin>> GetAllAdminsAsync()
        {
            return await _mediator.Send(new GetAllAdminsQuery());
        }

        public async Task UpdateAdminAsync(AdminUpdateModel admin, int adminId)
        {
            await _mediator.Send(new UpdateAdminCommand(admin.Username, admin.Password, admin.Email, admin.FullName, adminId));
        }
    }
}
