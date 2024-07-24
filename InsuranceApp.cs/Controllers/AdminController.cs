using InsuranceAppBLL.AdminService;
using InsuranceAppRLL.Entities;
using InsuranceMLL.AdminModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using UserModelLayer;

namespace InsuranceApp.cs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly ILogger<AdminController> _logger;

        public AdminController(IAdminService adminService,ILogger<AdminController> logger)
        {
            _adminService = adminService;
            _logger = logger;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<ResponseModel<Admin>>> CreateAdmin([FromBody] AdminRegistrationModel admin)
        {
            try
            {
                await _adminService.CreateAdminAsync(admin);

                // Log the registration event
                _logger.LogInformation($"Admin {admin.Email} registered.");

                // Prepare and return the response model
                var responseModel = new ResponseModel<Admin>
                {
                    Message = "Admin Added Successfully",
                    Status = true
                };
                return Ok(responseModel);
            }
            catch (Exception ex)
            {
                // Handle the exception and prepare the response model
                var responseModel = new ResponseModel<Admin>
                {
                    Message = ex.Message,
                    Status = false
                };
                return BadRequest(responseModel);
            }
        }

        
    }
}
