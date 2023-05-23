using Microsoft.AspNetCore.Mvc;

namespace Eravol.UIClient.Pages.Components.FooterComponent
{
	public class FooterComponent: ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync()
		{
			string Title = "Footer";
			return View("FooterComponent", Title);
		}
	}
}
