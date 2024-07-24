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
            LoginModel model = new LoginModel(request.Email,request.Password,request.Role);
            return await _repository.LoginAsync(model);
        }
    }
}
