using Eravol.UIClient.ViewModels.Users.Public;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using System.Data;
using System.Diagnostics.Metrics;

namespace Eravol.UIClient.Pages.Components.HeaderComponent
{
	public class HeaderComponent : ViewComponent
	{
		const string ADMIN = "Admin";
		const string FREELANCER = "Freelancer";
		const string CLIENT = "Client";

		public async Task<IViewComponentResult> InvokeAsync()
		{
			string role = null;

			if (User.IsInRole(ADMIN))
			{
				role = ADMIN;
			}
			else if (User.IsInRole(FREELANCER))
			{
				role = FREELANCER;
			}
			else if (User.IsInRole(CLIENT))
			{
				role = CLIENT;
			}

			UserInforViewModes? userInfo = new UserInforViewModes()
			{
				UserId = HttpContext.Session.GetString("UserId"),
				Email = HttpContext.Session.GetString("Email"),
				FullName= HttpContext.Session.GetString("Fullname"),
				PhoneNumber= HttpContext.Session.GetString("PhoneNumber"),
				Role= role,
				UserName = HttpContext.Session.GetString("Username")
			};
			return View("HeaderComponent", userInfo);
		}
	}
}
