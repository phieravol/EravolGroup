using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.Common;

namespace Eravol.UIClient.Pages.User
{
    public class LogoutModel : PageModel
    {
		public async Task<IActionResult> OnGetAsync()
		{
			HttpContext.Session.Clear();
			HttpContext.Session.Remove("AuthToken");
			HttpContext.Session.Remove("UserId");
			HttpContext.Session.Remove("LoginStatus");
			HttpContext.Session.Remove("Username");
			HttpContext.Session.Remove("Fullname");
			HttpContext.Session.Remove("Email");
			HttpContext.Session.Remove("PhoneNumber");
			HttpContext.Session.Remove("Roles");
			await HttpContext.Session.CommitAsync();
			return RedirectToPage("/Index");
		}
	}
}
