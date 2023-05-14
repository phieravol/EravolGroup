using Microsoft.AspNetCore.Mvc;

namespace Eravol.UIClient.Pages.Components.HomeBannerComponent
{
	public class HomeBannerComponent : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync()
		{
			string Title = "Home Page";
			return View("HomeBannerComponent", Title);
		}
	}
}
