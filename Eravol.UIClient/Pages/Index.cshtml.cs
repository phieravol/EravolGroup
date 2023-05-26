using Eravlol.UserWebApi.Data.Models;
using Eravol.UIClient.ViewModels.Users.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Eravol.UIClient.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> logger;

		public IndexModel(ILogger<IndexModel> logger)
		{
			this.logger = logger;
		}
		[BindProperty(SupportsGet = true)] UserInforViewModes UserInfo { get; set; }
		public async Task<IActionResult> OnGetAsync()
		{
			string? Username = User.Identity?.Name;

			if (Username == null) { return Page(); }

			UserInfo = new UserInforViewModes()
			{
				FullName = User.FindFirst(ClaimTypes.GivenName)?.Value,
				Email = User.FindFirst(ClaimTypes.Email)?.Value,
				PhoneNumber = User.FindFirst(ClaimTypes.MobilePhone)?.Value,
				Role = User.FindFirst(ClaimTypes.Role)?.Value
			};

			return Page();
		}
	}
}