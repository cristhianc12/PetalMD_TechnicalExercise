using System.Threading.Tasks;
using Application.DTO.Security;
using Application.Interface.Security;
using Microsoft.AspNetCore.Mvc;

namespace DistribuitedServices.Security.WebApi.Controllers
{
    /// <summary>
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsersAppService _usersAppService;
        /// <summary>
        /// </summary>
        /// <param name="usersAppService"></param>
        public AuthController(IUsersAppService usersAppService)
        {
            _usersAppService = usersAppService;
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <remarks>
        /// POST
        /// {
        ///    "Usuario": "cbuitrago",
        ///    "password": "PASSWORD",
        ///    "IP": "10.0.0.1",
        ///    "UserHostAddress": "10.0.0.1",
        ///    "SistemaOperativo": "Windows",
        ///    "Explorador": "Chrome"
        ///}
        /// </remarks>
        /// <param name="oCredentialDTO"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody] CredentialDTO oCredentialDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            oCredentialDTO.UserHostAddress = string.IsNullOrEmpty(oCredentialDTO.UserHostAddress) ? Request.HttpContext.Connection.RemoteIpAddress.ToString() : oCredentialDTO.UserHostAddress;
            oCredentialDTO.Server = Request.HttpContext.Connection.RemoteIpAddress.ToString();

            var jwt = await _usersAppService.Login(oCredentialDTO);
            if (string.IsNullOrEmpty(jwt))
            {
                return BadRequest("Invalid Payload");
            }

            if(!oCredentialDTO.UserId.Trim().ToLower().Equals("petalmdapiuser") || !oCredentialDTO.Password.Trim().Equals("UserTest.2021"))
            {

                return StatusCode(401, "Invalid Credentials");
            }

            return new OkObjectResult(jwt);
        }
    }
}
