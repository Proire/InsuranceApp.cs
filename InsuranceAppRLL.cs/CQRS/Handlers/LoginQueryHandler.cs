using InsuranceAppRLL.CQRS.Queries;
using InsuranceAppRLL.CustomExceptions;
using InsuranceAppRLL.Repositories.Implementations.AdminRepository;
using InsuranceAppRLL.Repositories.Interfaces;
using InsuranceMLL;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModelLayer;

namespace InsuranceAppRLL.CQRS.Handlers
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, string>
    {
        private readonly ILoginRepository _repository;

        public LoginQueryHandler(ILoginRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            string token;

            // Perform login logic based on the role
            switch (request.Role.ToLower())
            {
                case "admin":
                    token = await _repository.LoginAdminAsync(new LoginModel(request.Email, request.Password, request.Role));
                    break;
                case "insuranceagent":
                    token = await _repository.LoginInsuranceAgentAsync(new LoginModel(request.Email, request.Password, request.Role ));
                    break;
                case "employee":
                    token = await _repository.LoginEmployeeAsync(new LoginModel(request.Email, request.Password, request.Role));
                    break;
                case "customer":
                    token = await _repository.LoginCustomerAsync(new LoginModel(request.Email, request.Password, request.Role));
                    break;
                default:
                    throw new LoginException("Invalid role specified.");
            }

            return token;
        }
    }
}
