using Microsoft.AspNetCore.Mvc;

namespace Eravol.UIClient.Pages.Components.DashboardSideBarComponent
{
	public class DashboardSideBarComponent : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync()
		{
			string Title = "Dashboard SideBar";
			return View("DashboardSideBarComponent", Title);
		}
	}
}
