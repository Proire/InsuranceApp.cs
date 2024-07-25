﻿using InsuranceAppBLL.SchemeService;
using InsuranceAppRLL.CustomExceptions;
using InsuranceAppRLL.Entities;
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
        [Route("/plan/{planId}/schemes")]
        public async Task<ActionResult<ResponseModel<IEnumerable<Scheme>>>> GetAllSchemesByPlanIdAsync(int planId)
        {
            try
            {
                var schemes = await _schemeService.GetAllSchemesForPlanAsync(planId);

                if (schemes == null || !schemes.Any())
                {
                    return new ResponseModel<IEnumerable<Scheme>>
                    {
                        Data = new List<Scheme>(),
                        Message = "No schemes found for the plan.",
                        Status = false
                    };
                }

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
                    Data = new List<Scheme>(),
                    Message = ex.Message,
                    Status = false
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<IEnumerable<Scheme>>
                {
                    Data = new List<Scheme>(),
                    Message = "An unexpected error occurred. " + ex.Message,
                    Status = false
                };
            }
        }

        [HttpGet("{schemeId}")]
        public async Task<ActionResult<ResponseModel<Scheme>>> GetSchemeByIdAsync(int schemeId)
        {
            try
            {
                var scheme = await _schemeService.GetSchemeByIdAsync(schemeId);
                if (scheme == null)
                {
                    return new ResponseModel<Scheme>
                    {
                        Data = new Scheme(),
                        Message = $"No scheme found with id: {schemeId}",
                        Status = false
                    };
                }
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
                    Data = new Scheme(),
                    Message = ex.Message,
                    Status = false
                };
            }
        }
    }
}
