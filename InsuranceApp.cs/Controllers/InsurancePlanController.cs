<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceApp.Controllers
{
    [Route("InsuranceApp/InsurancePlan")]
    [ApiController]
    public class InsurancePlanController : ControllerBase
    {
=======
﻿using InsuranceAppBLL.InsurancePlanService;
using InsuranceAppRLL.Entities;
using InsuranceMLL.InsurancePlanModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserModelLayer;

namespace InsuranceApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InsurancePlanController : ControllerBase
    {
        private readonly IInsurancePlanService _insurancePlanService;
        private readonly ILogger<InsurancePlanController> _logger;

        public InsurancePlanController(IInsurancePlanService insurancePlanService, ILogger<InsurancePlanController> logger)
        {
            _insurancePlanService = insurancePlanService;
            _logger = logger;
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<ResponseModel<string>>> CreateInsurancePlan([FromBody] InsurancePlanCreationModel insurancePlan)
        {
            try
            {
                await _insurancePlanService.CreateInsurancePlanAsync(insurancePlan);

                // Log the creation event
                _logger.LogInformation($"Insurance Plan '{insurancePlan.PlanName}' created.");

                // Prepare and return the response model
                var responseModel = new ResponseModel<string>
                {
                    Message = "Insurance Plan Added Successfully",
                    Status = true
                };
                return Ok(responseModel);
            }
            catch (Exception ex)
            {
                // Handle the exception and prepare the response model
                var responseModel = new ResponseModel<string>
                {
                    Message = ex.Message,
                    Status = false
                };
                return BadRequest(responseModel);
            }
        }

        [HttpDelete("/delete/{planId}")]
        public async Task<ResponseModel<string>> DeleteInsurancePlan(int planId)
        {
            try
            {
                await _insurancePlanService.DeleteInsurancePlanAsync(planId);
                return new ResponseModel<string>
                {
                    Message = "Insurance Plan deleted successfully",
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

        [HttpGet("/{planId}")]
        public async Task<ResponseModel<InsurancePlan>> GetInsurancePlanById(int planId)
        {
            try
            {
                var insurancePlan = await _insurancePlanService.GetInsurancePlanByIdAsync(planId);
                if (insurancePlan == null)
                {
                    return new ResponseModel<InsurancePlan>
                    {
                        Data = new InsurancePlan(),
                        Message = $"No insurance plan found with id: {planId}",
                        Status = false
                    };
                }
                return new ResponseModel<InsurancePlan>
                {
                    Data = insurancePlan,
                    Message = "Insurance Plan retrieved successfully",
                    Status = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<InsurancePlan>
                {
                    Data = new InsurancePlan(),
                    Message = ex.Message,
                    Status = false
                };
            }
        }

        [HttpGet("/plans")]
        public async Task<ResponseModel<IEnumerable<InsurancePlan>>> GetAllInsurancePlans()
        {
            try
            {
                var insurancePlans = await _insurancePlanService.GetAllInsurancePlansAsync();
                return new ResponseModel<IEnumerable<InsurancePlan>>
                {
                    Data = insurancePlans,
                    Message = "Insurance Plans retrieved successfully",
                    Status = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<IEnumerable<InsurancePlan>>
                {
                    Data = new List<InsurancePlan>(),
                    Message = ex.Message,
                    Status = false
                };
            }
        }

        [HttpPut("/updateInsurancePlan/{planId}")]
        public async Task<ResponseModel<string>> UpdateInsurancePlan([FromBody] UpdateInsurancePlanModel insurancePlanModel, int planId)
        {
            try
            {
                await _insurancePlanService.UpdateInsurancePlanAsync(insurancePlanModel, planId);
                return new ResponseModel<string>
                {
                    Message = "Insurance Plan updated successfully",
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

>>>>>>> 934d4899729eba4fa6d9b04ca3e54115e79f8128
    }
}
