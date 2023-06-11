using Microsoft.AspNetCore.Mvc;

namespace Eravol.UIClient.Pages.Components.MenuHeaderComponent
{
	public class MenuHeaderComponent : ViewComponent
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
			return View("MenuHeaderComponent", role);
		}
	}
}