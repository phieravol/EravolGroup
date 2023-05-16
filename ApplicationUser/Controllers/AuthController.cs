﻿using Eravol.UserWebApi.System;
using Eravol.UserWebApi.ViewModels.System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;

namespace Eravol.UserWebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
        //https://localhost:7053/
		private const string DOMAIN = "https://localhost";
		private const string PORT = "7053";

		private readonly IAccountService accountService;

		public AuthController(IAccountService accountService)
		{
			this.accountService = accountService;
		}

		[HttpPost("authenticate")]
		[AllowAnonymous]
		public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var resultToken = await accountService.Authenticate(request);
			if (string.IsNullOrEmpty(resultToken))
			{
				return BadRequest("Username or password is incorrect");
			}
			return Ok(new {token = resultToken});
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
			string path = "";
			string redirectUrl = $"{DOMAIN}://{PORT}/{path}";

			return Redirect(redirectUrl);
        }
	}
}
