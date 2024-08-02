using InsuranceAppBLL.CommissionService;
using InsuranceAppRLL.CustomExceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserModelLayer;

namespace InsuranceApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommissionsController : ControllerBase
    {
        private readonly ICommissionService _commissionService;
        private readonly ILogger<CommissionsController> _logger;

        public CommissionsController(ICommissionService commissionService, ILogger<CommissionsController> logger)
        {
            _commissionService = commissionService;
            _logger = logger;
        }

        [Authorize(AuthenticationSchemes = "AdminScheme", Roles = "Admin")]
        [Authorize(AuthenticationSchemes = "InsuranceAgentScheme", Roles = "InsuranceAgent")]
        [HttpGet("agent/{agentId}")]
        public async Task<ActionResult<ResponseModel<double>>> GetTotalCommissionForAgentAsync(int agentId)
        {
            _logger.LogInformation("Request received to get total commission for agent with ID: {AgentId}", agentId);
            try
            {
                var totalCommission = await _commissionService.GetTotalCommissionForAgentAsync(agentId);
                _logger.LogInformation("Successfully retrieved total commission for agent with ID: {AgentId}", agentId);

                return new ResponseModel<double>
                {
                    Data = totalCommission,
                    Message = "Total commission retrieved successfully.",
                    Status = true
                };
            }
            catch (CommissionException ex)
            {
                _logger.LogWarning("CommissionException: {Message}", ex.Message);
                return new ResponseModel<double>
                {
                    Data = 0,
                    Message = ex.Message,
                    Status = false
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while getting total commission for agent with ID: {AgentId}. Error: {Message}", agentId, ex.Message);
                return new ResponseModel<double>
                {
                    Data = 0,
                    Message = "An unexpected error occurred. " + ex.Message,
                    Status = false
                };
            }
        }
    }
}
