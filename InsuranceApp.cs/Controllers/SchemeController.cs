using InsuranceAppBLL.SchemeService;
using InsuranceAppRLL.CustomExceptions;
using InsuranceAppRLL.Entities;
using InsuranceMLL.SchemeModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserModelLayer;

namespace InsuranceApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchemeController : ControllerBase
    {
        private readonly ISchemeService _schemeService;
        private readonly ILogger<SchemeController> _logger;

        public SchemeController(ISchemeService schemeService, ILogger<SchemeController> logger)
        {
            _schemeService = schemeService;
            _logger = logger;
        }

        [HttpGet]
        [Route("plan/{planId}/schemes")]
        public async Task<ActionResult<ResponseModel<IEnumerable<Scheme>>>> GetAllSchemesByPlanIdAsync(int planId)
        {
            try
            {
                var schemes = await _schemeService.GetAllSchemesForPlanAsync(planId);

                // Return the response model with success status
                return new ResponseModel<IEnumerable<Scheme>>
                {
                    Data = schemes,
                    Message = "Schemes retrieved successfully.",
                    Status = true
                };
            }
            catch (SchemeException ex)
            {
                return new ResponseModel<IEnumerable<Scheme>>
                {
                    Message = ex.Message,
                    Status = false
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<IEnumerable<Scheme>>
                {
                    Message = "An unexpected error occurred. " + ex.Message,
                    Status = false
                };
            }
        }

        [HttpGet("plan/{schemeId}")]
        public async Task<ActionResult<ResponseModel<Scheme>>> GetSchemeByIdAsync(int schemeId)
        {
            try
            {
                var scheme = await _schemeService.GetSchemeByIdAsync(schemeId);
               
                return new ResponseModel<Scheme>
                {
                    Data = scheme,
                    Message = "Scheme retrieved successfully",
                    Status = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<Scheme>
                {
                    Message = ex.Message,
                    Status = false
                };
            }
        }

       // [Authorize(AuthenticationSchemes = "EmployeeScheme", Roles = "Employee")]
        [HttpPost]
        [Route("plan/create")]
        public async Task<ActionResult<ResponseModel<Scheme>>> CreateScheme([FromBody] SchemeRegistrationModel schemeModel)
        {
            try
            {
                await _schemeService.CreateSchemeAsync(schemeModel);

                // Log the creation event
                _logger.LogInformation($"Scheme {schemeModel.SchemeName} created.");

                // Prepare and return the response model
                var responseModel = new ResponseModel<Scheme>
                {
                    Message = "Scheme Created Successfully",
                    Status = true
                };
                return Ok(responseModel);
            }
            catch (Exception ex)
            {
                // Handle the exception and prepare the response model
                var responseModel = new ResponseModel<Scheme>
                {
                    Message = ex.Message,
                    Status = false
                };
                return BadRequest(responseModel);
            }
        }

       // [Authorize(AuthenticationSchemes = "EmployeeScheme", Roles = "Employee")]
        [HttpPut("plan/{schemeId}")]
        public async Task<ActionResult<ResponseModel<string>>> UpdateScheme([FromBody] SchemeUpdateModel schemeModel, int schemeId)
        {
            try
            {
                await _schemeService.UpdateSchemeAsync(schemeModel, schemeId);

                // Log the update event
                _logger.LogInformation($"Scheme {schemeId} updated.");

                // Prepare and return the response model
                var responseModel = new ResponseModel<string>
                {
                    Message = "Scheme Updated Successfully",
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

       // [Authorize(AuthenticationSchemes = "EmployeeScheme", Roles = "Employee")]
        [HttpDelete("plan/{schemeId}")]
        public async Task<ActionResult<ResponseModel<string>>> DeleteScheme(int schemeId)
        {
            try
            {
                await _schemeService.DeleteSchemeAsync(schemeId);

                // Log the deletion event
                _logger.LogInformation($"Scheme {schemeId} deleted.");

                // Prepare and return the response model
                var responseModel = new ResponseModel<string>
                {
                    Message = "Scheme Deleted Successfully",
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
    }
}

