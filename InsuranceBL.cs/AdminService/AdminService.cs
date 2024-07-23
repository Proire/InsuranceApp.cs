using InsuranceAppRLL.CQRS.Commands.AdminCommands;
using InsuranceAppRLL.CQRS.Queries;
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

        public async Task<string> LoginAdminAsync(LoginModel login)
        {
            var command = new LoginUserQuery(login.Email, login.Password);
            return await _mediator.Send(command);
        }
    }
}
