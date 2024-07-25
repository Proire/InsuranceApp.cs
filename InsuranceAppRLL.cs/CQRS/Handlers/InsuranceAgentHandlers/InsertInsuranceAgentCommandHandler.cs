using InsuranceAppRLL.CQRS.Commands.InsuranceAgentCommands;
using InsuranceAppRLL.Entities;
using InsuranceAppRLL.Repositories.Interfaces.InsuranceAgentRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CQRS.Handlers.InsuranceAgentHandlers
{
    public class InsertInsuranceAgentCommandHandler : IRequestHandler<InsertInsuranceAgentCommand, InsuranceAgent>
    {
        private readonly IInsuranceAgentCommandRepository _repository;

        public InsertInsuranceAgentCommandHandler(IInsuranceAgentCommandRepository repository)
        {
            _repository = repository;
        }

        public async Task<InsuranceAgent> Handle(InsertInsuranceAgentCommand request, CancellationToken cancellationToken)
        {
            var insuranceAgent = new InsuranceAgent
            {
                Username = request.Username,
                Password = request.Password,
                Email = request.Email,
                FullName = request.FullName,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.RegisterInsuranceAgentAsync(insuranceAgent);
            return insuranceAgent;
        }
    }
}
