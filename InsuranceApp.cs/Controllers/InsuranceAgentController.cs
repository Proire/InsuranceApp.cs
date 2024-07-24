using InsuranceAppBLL.InsuranceAgentService;
using InsuranceAppRLL.Entities;
using InsuranceMLL.InsuranceAgentModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserModelLayer;

namespace InsuranceApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InsuranceAgentController : ControllerBase
    {
        private readonly IInsuranceAgentService _insuranceAgentService;
        private readonly ILogger<InsuranceAgentController> _logger;

        public InsuranceAgentController(IInsuranceAgentService insuranceAgentService, ILogger<InsuranceAgentController> logger)
        {
            _insuranceAgentService = insuranceAgentService;
            _logger = logger;
        }

        [Authorize(AuthenticationSchemes = "AdminScheme", Roles = "Admin")]
        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<ResponseModel<InsuranceAgent>>> CreateInsuranceAgent([FromBody] InsuranceAgentRegistrationModel agent)
        {
            try
            {
                await _insuranceAgentService.RegisterInsuranceAgentAsync(agent);

                // Log the registration event
                _logger.LogInformation($"Insurance Agent {agent.Email} registered.");

                // Prepare and return the response model
                var responseModel = new ResponseModel<InsuranceAgent>
                {
                    Message = "Insurance Agent Added Successfully",
                    Status = true
                };
                return Ok(responseModel);
            }
            catch (Exception ex)
            {
                // Handle the exception and prepare the response model
                var responseModel = new ResponseModel<InsuranceAgent>
                {
                    Message = ex.Message,
                    Status = false
                };
                return BadRequest(responseModel);
            }
        }

        [HttpGet("/get/agents")]
        public async Task<ResponseModel<IEnumerable<InsuranceAgent>>> GetAllInsuranceAgents()
        {
            try
            {
                var agents = await _insuranceAgentService.GetAllInsuranceAgents();
                return new ResponseModel<IEnumerable<InsuranceAgent>>
                {
                    Data = agents,
                    Message = "Insurance agents retrieved successfully",
                    Status = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<IEnumerable<InsuranceAgent>>
                {
                    Data = null,
                    Message = ex.Message,
                    Status = false
                };
            }
        }

        [HttpGet("{agentId}")]
        public async Task<ResponseModel<InsuranceAgent>> GetAgentById(int agentId)
        {
            try
            {
                var agent = await _insuranceAgentService.GetInsuranceAgentAsync(agentId);
                if (agent == null)
                {
                    return new ResponseModel<InsuranceAgent>
                    {
                        Data = null,
                        Message = $"No agent found with id: {agentId}",
                        Status = false
                    };
                }
                return new ResponseModel<InsuranceAgent>
                {
                    Data = agent,
                    Message = "Agent retrieved successfully",
                    Status = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<InsuranceAgent>
                {
                    Data = null,
                    Message = ex.Message,
                    Status = false
                };
            }
        }
    }
}
