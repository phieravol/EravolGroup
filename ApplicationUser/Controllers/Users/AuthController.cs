using Eravol.UserWebApi.System;
using Eravol.UserWebApi.ViewModels.System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;

namespace Eravol.WebApi.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAccountService accountService;

        public AuthController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
        {
            bool loginStatus = true;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultToken = await accountService.Authenticate(request);

            if (!accountService.isAccountExisted(request))
            {
                loginStatus = false;
                resultToken = $"Can not find user have username @{request.UserName}";
            }
            else if (resultToken is null)
            {
                loginStatus = false;
                resultToken = "Username or password is incorrect";
            }
            return Ok(new {
                loginStatus = loginStatus,
                loginResult = resultToken 
            });
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await accountService.Registration(request);
            if (!result)
            {
                return BadRequest("Register unsuccessfull!");
            }
            return Ok();
        }
    }
}
