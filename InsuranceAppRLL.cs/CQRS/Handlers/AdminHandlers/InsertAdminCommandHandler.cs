using InsuranceAppRLL.Entities;
using InsuranceAppRLL.CQRS.Commands.AdminCommands;
using InsuranceAppRLL.Repositories.Interfaces.AdminRepository;

namespace InsuranceAppRLL.CQRS.Handlers.AdminHandlers
{
    public class InsertAdminCommandHandler : MediatR.IRequestHandler<InsertAdminCommand, Admin>
    {
        private readonly IAdminCommandRepository _repository;

        public InsertAdminCommandHandler(IAdminCommandRepository repository)
        {
            _repository = repository;
        }

        public async Task<Admin> Handle(InsertAdminCommand request, CancellationToken cancellationToken)
        {
            var admin = new Admin
            {
                Username = request.Username,
                Password = request.Password, 
                Email = request.Email,
                FullName = request.FullName,
            };

            await _repository.RegisterAdminAsync(admin);
            return admin;
        }
    }
}
