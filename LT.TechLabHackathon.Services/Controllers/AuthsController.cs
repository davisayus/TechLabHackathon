using LT.TechLabHackathon.Core.v1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using LT.TechLabHackathon.Domain.Contracts;
using LT.TechLabHackathon.Shared.Helpers;
using static LT.TechLabHackathon.Shared.DTOs.Records;
using LT.TechLabHackathon.Shared.DTOs;
using System.IdentityModel.Tokens.Jwt;

namespace LT.TechLabHackathon.Services.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        private readonly AuthCore _core;
        public AuthsController(IConfiguration configuration, IAuthRepository repository, ILogger<AuthCore> logger)
        {
            _core = new AuthCore(configuration, repository, logger);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<ResponseService<LoginResponseDto>>> Login([FromBody] LoginRequestDto loginRequest)
        {
            var response = await _core.Login(loginRequest.UserName.ToLower(), loginRequest.Password);
            return StatusCode((int)response.StatusHttp, response);
        }

        [AllowAnonymous]
        [HttpPost("requestdynamicpassword/{userName}")]
        public async Task<ActionResult<ResponseService<bool>>> RequestDynamicPassword(string userName)
        {
            var response = await _core.RequestDynamicPassword(userName.ToLower());
            return StatusCode((int)response.StatusHttp, response);
        }

        [AllowAnonymous]
        [HttpPost("logindynamic")]
        public async Task<ActionResult<ResponseService<LoginResponseDto>>> LoginDynamicPassword([FromBody] LoginRequestDto loginRequest)
        {
            var response = await _core.LoginWithDynamicPassword(loginRequest.UserName.ToLower(), loginRequest.Password);
            return StatusCode((int)response.StatusHttp, response);
        }

        [AllowAnonymous]
        [HttpPatch("setpassword")]
        public async Task<ActionResult<ResponseService<bool>>> SetPassword([FromBody] LoginRequestDto loginRequest)
        {
            var response = await _core.SetPassword(loginRequest.UserName.ToLower(), loginRequest.Password);
            return StatusCode((int)response.StatusHttp, response);
        }

        [HttpPatch("changepassword")]
        public async Task<ActionResult<ResponseService<bool>>> ChangePassword([FromBody] LoginChangePasswordDto loginRequest)
        {
            var response = await _core.ChangePassword(loginRequest.UserName.ToLower(), loginRequest.CurrentPassword, loginRequest.NewPassword);
            return StatusCode((int)response.StatusHttp, response);
        }

        [HttpGet("userauthenticated")]
        public async Task<ActionResult<ResponseService<UserDto>>> GetUserAuthenticated()
        {
            if (HttpContext.User.Identity is null || !HttpContext.User.Identity.IsAuthenticated)
                return StatusCode(401, new ResponseService<UserDto>(true, "User not authenticated", System.Net.HttpStatusCode.Unauthorized, new UserDto(), 0));

            var userEmailClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)!.Value ?? string.Empty;
            var response = await _core.GetUserAuthenticated(userEmailClaim);
            return StatusCode((int)response.StatusHttp, response);
        }

        [AllowAnonymous]
        [HttpPost("renewtoken")]
        public async Task<ActionResult<ResponseService<LoginResponseDto>>> RenewToken([FromBody] LoginRenewToken loginRenewToken)
        {
            var response = await _core.RenewToken(loginRenewToken);
            return StatusCode((int)response.StatusHttp, response);
        }


        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<ResponseService<LoginResponseDto>>> RegisterAsync([FromBody] UserCreateDto userCreate)
        {
            var response = await _core.Register(userCreate);
            return StatusCode((int)response.StatusHttp, response);
        }

    }
}
