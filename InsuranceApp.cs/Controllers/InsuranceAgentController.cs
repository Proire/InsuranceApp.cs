using InsuranceAppBLL.InsuranceAgentService;
using InsuranceAppRLL.Entities;
using InsuranceMLL.InsuranceAgentModels;
using Microsoft.AspNetCore.Authorization;
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
        [Route("agent_user/register")]
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

        [Authorize(AuthenticationSchemes = "AdminScheme", Roles = "Admin")]
        [HttpGet("agent_user/agents")]
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
                    Message = ex.Message,
                    Status = false
                };
            }
        }

        [Authorize(AuthenticationSchemes = "InsuranceAgentScheme", Roles = "InsuranceAgent")]
        [Authorize(AuthenticationSchemes = "AdminScheme", Roles = "Admin")]
        [HttpGet("agent_user/{agentId}")]
        public async Task<ResponseModel<InsuranceAgent>> GetAgentById(int agentId)
        {
            try
            {
                var agent = await _insuranceAgentService.GetInsuranceAgentAsync(agentId);
               
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

        [Authorize(AuthenticationSchemes = "AdminScheme", Roles = "Admin")]
        [HttpPut("agent_user/update/{agentId}")]
        public async Task<ResponseModel<string>> UpdateAgent([FromBody] AgentUpdateModel agentModel, int agentId)
        {
            try
            {
                await _insuranceAgentService.UpdateAgentAsync(agentModel, agentId);
                return new ResponseModel<string>
                {
                    Message = "Insurance Agent updated successfully",
                    Status = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<string>
                {
                    Message = ex.Message,
                    Status = false
                };
            }
        }

        [Authorize(AuthenticationSchemes = "AdminScheme", Roles = "Admin")]
        [HttpDelete("agent_user/delete/{agentId}")]
        public async Task<ResponseModel<string>> DeleteAgent(int agentId)
        {
            try
            {
                await _insuranceAgentService.DeleteAgentAsync(agentId);
                return new ResponseModel<string>
                {
                    Message = "Insurance Agent deleted successfully",
                    Status = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<string>
                {
                    Message = ex.Message,
                    Status = false
                };
            }
        }
    }
}
