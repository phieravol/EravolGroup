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
		private readonly IAuthenticationSchemeProvider authenticationSchemeProvider;

		public LoginModel(ILoginApiClient loginApiClient, IConfiguration configuration, IAuthenticationSchemeProvider authenticationSchemeProvider)
		{
			this.loginApiClient = loginApiClient;
			this.configuration = configuration;
			this.authenticationSchemeProvider = authenticationSchemeProvider;
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

			var tokenHandler = new JwtSecurityTokenHandler();
			var jwtToken = tokenHandler.ReadJwtToken(token);

			UserInforViewModes? userInfo = new UserInforViewModes()
			{
				UserId = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value,
				UserName = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value,
				Email = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
				FullName = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value,
				PhoneNumber = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.MobilePhone)?.Value,
				Role = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value,
				IsLoginSuccess = true
			};

			SetSessionUser(userInfo, token);

			switch (userInfo.Role)
			{
				case "Admin":
					{
						return RedirectToPage("/dashboard/insight/admin/index");
					}
				case "Client":
					{
						return RedirectToPage("/dashboard/insight/clients/index");
					}
				case "Freelancer":
					{
						return RedirectToPage("/dashboard/insight/freelancers/index");
					}
				default:
					{
						return Page();
					}
			}
		}

		/// <summary>
		/// Set session information of user
		/// </summary>
		/// <param name="token"></param>
		private void SetSessionUser(UserInforViewModes userInfo, string token)
		{
			
			HttpContext.Session.SetString("AuthToken", token);
			HttpContext.Session.SetString("UserId", userInfo.UserId);
			HttpContext.Session.SetString("LoginStatus", userInfo.IsLoginSuccess.ToString());
			HttpContext.Session.SetString("Username", userInfo.UserName);
			HttpContext.Session.SetString("Fullname", userInfo.FullName);
			HttpContext.Session.SetString("Email", userInfo.Email);
			HttpContext.Session.SetString("PhoneNumber", userInfo.PhoneNumber);
			HttpContext.Session.SetString("Roles", userInfo.Role);

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

