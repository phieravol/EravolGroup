using Eravlol.UserWebApi.Data.Models;
using Eravol.UIClient.Repositories.Users;
using Eravol.UIClient.ViewModels.Users.Public;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using NuGet.Common;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Eravol.UIClient.Pages.User
{
    public class LoginModel : PageModel
    {
		private readonly ILoginApiClient loginApiClient;
		private readonly IConfiguration configuration;

		public LoginModel(ILoginApiClient loginApiClient, IConfiguration configuration)
		{
			this.loginApiClient = loginApiClient;
			this.configuration = configuration;
		}

		public async Task<IActionResult> OnGetAsync(string username, string password)
		{
			if (!ModelState.IsValid)
			{
				//return bad request if invalid input
				return BadRequest(ModelState);
			}

			LoginRequest request = new LoginRequest()
			{
				UserName = username,
				Password = password,
				RememberMe = true
			};
			
			//call login api client
			var response = await loginApiClient.Authenticate(request);
			var loginStatus = response.loginStatus;

			var responseData = new
			{
				loginStatus = "false",
				loginResult = response.loginResult.ToString()
			};

			if (!loginStatus)
			{
				return new JsonResult(responseData);
			}
			var token = response.loginResult;
			HttpContext.Session.SetString("AuthToken", token);

			var userPrincipal = this.ValidateToken(token);

			var authProperties = new AuthenticationProperties
			{
				ExpiresUtc = DateTimeOffset.UtcNow.AddDays(60),
				IsPersistent = true
			};

			await HttpContext.SignInAsync(
				CookieAuthenticationDefaults.AuthenticationScheme,
				userPrincipal,
				authProperties);

			var jsonData = new {
				userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
				loginStatus = "true",
				username = User.Identity?.Name,
				fullname = User.FindFirst(ClaimTypes.GivenName)?.Value,
				email = User.FindFirst(ClaimTypes.Email)?.Value,
				phoneNumber = User.FindFirst(ClaimTypes.MobilePhone)?.Value,
				roles = User.FindFirst(ClaimTypes.Role)?.Value
			};

			return new JsonResult(jsonData);
		}

		
		public async Task<IActionResult> LogoutAsync()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToPage("/Index");
		}

		private ClaimsPrincipal ValidateToken(string jwtToken)
		{
			IdentityModelEventSource.ShowPII = true;
			SecurityToken validateToken;

			TokenValidationParameters validationPrarameters = new TokenValidationParameters();

			validationPrarameters.ValidateLifetime = true;
			validationPrarameters.ValidAudience = configuration["JWT:ValidAudience"];
			validationPrarameters.ValidIssuer = configuration["JWT:ValidIssuer"];
			validationPrarameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

			ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationPrarameters, out validateToken);
			return principal;
		}
	}
}

