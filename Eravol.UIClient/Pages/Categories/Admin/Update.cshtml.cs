using Eravol.UIClient.Repositories.General;
using Eravol.UIClient.ViewModels.General;
using Eravol.WebApi.Data.Models;
using Eravol.WebApi.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics.Metrics;

namespace Eravol.UIClient.Pages.Categories.Admin
{
    public class UpdateModel : PageModel
    {
		const string BASE_URL = "https://localhost:7259";
		string RELATIVE_PATH_URL = $"/api/Admin/Categories";
		const string HTTP_GET = "GET";
		const string HTTP_PUT = "PUT";
		const string HTTP_POST = "POST";
		const string ROLE_ADMIN = "admin";
		const string ROLE_MEMBER = "member";

		private readonly IClientsRequestService<Category> requestService;

		public UpdateModel(IClientsRequestService<Category> requestService)
		{
			this.requestService = requestService;
		}

		[BindProperty] public Category? category { get; set; }
		[BindProperty(SupportsGet = true)] public int? CategoryId { get; set; }

		public async Task<IActionResult> OnGetAsync()
        {
			if (CategoryId == null)
			{
				return NotFound();
			}

			string cateIdStr = $"CategoryId={CategoryId}";

			CommonClientsRequest<Category> getterRequest = new CommonClientsRequest<Category>()
			{
				httpBaseUrl = BASE_URL,
				httpRelativePath = $"{RELATIVE_PATH_URL}/GetCategory?{cateIdStr}",
				httpMethod = HTTP_GET,
				id = CategoryId
			};
			category = await requestService.HandleClientsRequest<CommonClientsRequest<Category>, Category?, Category>(getterRequest);
			if (category == null)
			{
				return NotFound();
			}
			
			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			CommonClientsRequest<Category> posterRequest = new CommonClientsRequest<Category>()
			{
				httpBaseUrl = BASE_URL,
				httpRelativePath = $"{RELATIVE_PATH_URL}/{CategoryId}",
				httpMethod = HTTP_PUT,
				Data = category
			};

			HttpResponseMessage createResponse = await requestService.HandleClientsRequest<CommonClientsRequest<Category>, HttpResponseMessage, Category>(posterRequest);
			return RedirectToPage("./Index");
		}
    }
}
