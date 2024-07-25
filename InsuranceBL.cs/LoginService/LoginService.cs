using InsuranceAppRLL.CQRS.Queries;
using InsuranceMLL;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppBLL.LoginService
{
    public class LoginService : ILoginService
    {
        private readonly IMediator _mediator;

        public LoginService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<string> LoginAsync(LoginModel login)
        {
            var command = new LoginQuery(login.Email, login.Password, login.Role);
            return await _mediator.Send(command);
        }
    }
}
