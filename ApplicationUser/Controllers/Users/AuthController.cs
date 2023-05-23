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
            bool loginStatus = false;
            string resultLogin = "";
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!accountService.isAccountExisted(request))
            {
                resultLogin = $"Can not find user have username @{request.UserName}";
            } 
            else
            {
                resultLogin = await accountService.Authenticate(request);
                if (resultLogin is null)
                {
                    resultLogin = "Username or password is incorrect";
                }
                else
                {
                    loginStatus = true;
                }
            }
            
            return Ok(new {
                loginStatus = loginStatus,
                loginResult = resultLogin 
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
