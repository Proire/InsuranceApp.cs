using InsuranceAppBLL.AdminService;
using InsuranceAppRLL.Entities;
using InsuranceMLL.AdminModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Security.Claims;
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
        [Route("admin_user/register")]
        public async Task<ActionResult<ResponseModel<string>>> CreateAdmin([FromBody] AdminRegistrationModel admin)
        {
            try
            {
                await _adminService.CreateAdminAsync(admin);

                // Log the registration event
                _logger.LogInformation($"Admin {admin.Email} registered.");

                // Prepare and return the response model
                var responseModel = new ResponseModel<string>
                {
                    Message = "Admin Added Successfully",
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

        [Authorize(AuthenticationSchemes = "AdminScheme", Roles = "Admin")]
        [HttpPut("admin_user/update")]
        public async Task<ResponseModel<string>> UpdateAdmin([FromBody] AdminUpdateModel adminModel)
        {
            try
            {
                int adminId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                await _adminService.UpdateAdminAsync(adminModel, adminId);
                return new ResponseModel<string>
                {
                    Message = "Admin updated successfully",
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
        [HttpDelete("admin_user/delete")]
        public async Task<ResponseModel<string>> DeleteAdmin()
        {
            try
            {
                int adminId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                await _adminService.DeleteAdminAsync(adminId);
                return new ResponseModel<string>
                {
                    Message = "Admin deleted successfully",
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
        [HttpGet("admin_user")]
        public async Task<ResponseModel<Admin>> GetAdminById()
        {
            try
            {
                int adminId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                var admin = await _adminService.GetAdminByIdAsync(adminId);
                return new ResponseModel<Admin>
                {
                    Data = admin,
                    Message = "Admin retrieved successfully",
                    Status = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<Admin>
                {
                    Message = ex.Message,
                    Status = false
                };
            }
        }

        [Authorize(AuthenticationSchemes = "AdminScheme", Roles = "Admin")]
        [HttpGet("admin_user/admins")]
        public async Task<ResponseModel<IEnumerable<Admin>>> GetAllAdmins()
        {
            try
            {
                var admins = await _adminService.GetAllAdminsAsync();
                return new ResponseModel<IEnumerable<Admin>>
                {
                    Data = admins,
                    Message = "Admins retrieved successfully",
                    Status = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<IEnumerable<Admin>>
                {
                    Message = ex.Message,
                    Status = false
                };
            }
        }
    }
}
