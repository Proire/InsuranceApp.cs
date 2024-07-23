using InsuranceApp.cs.Controllers;
using InsuranceAppBLL.AdminService;
using InsuranceAppBLL.LoginService;
using InsuranceMLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserModelLayer;

namespace InsuranceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILoginService loginService, ILogger<LoginController> logger)
        {
            _loginService = loginService;
            _logger = logger;
        }


        [HttpPost]
        public async Task<ActionResult<ResponseModel<string>>> LoginAsync([FromBody] LoginModel model)
        {
            try
            {
                // Call the asynchronous Login method from the business logic layer (BLL)
                string token = await _loginService.LoginAsync(model);

                // Prepare the response model with user data
                var responseModel = new ResponseModel<string>
                {
                    Message = "Logged In Successfully!",
                    Data = token, // Return the token
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
                    Data = "Try Again",
                    Status = false
                };

                return BadRequest(responseModel);
            }
        }
    }
}
