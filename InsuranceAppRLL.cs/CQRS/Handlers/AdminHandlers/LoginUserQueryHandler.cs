using InsuranceAppRLL.CQRS.Queries;
using InsuranceAppRLL.CustomExceptions;
using InsuranceAppRLL.Repositories.Implementations.AdminRepository;
using InsuranceAppRLL.Repositories.Interfaces.AdminRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModelLayer;

namespace InsuranceAppRLL.CQRS.Handlers.AdminHandlers
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, string>
    {
        private readonly IAdminQueryRepository _repository;

        public LoginUserQueryHandler(IAdminQueryRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {

            // Perform login logic
            var login = new LoginModel(request.Email, request.Password);
            var token = await _repository.LoginAdminAsync(login);

            if (string.IsNullOrEmpty(token))
            {
                throw new AdminException("Invalid email or password.");
            }

            return token;
        }
    }
}
