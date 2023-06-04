using Eravol.UIClient.Repositories.General;
using Eravol.UIClient.ViewModels.General;
using Eravol.WebApi.Data.Models;
using Eravol.WebApi.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics.Metrics;

namespace Eravol.UIClient.Pages.Categories.Admin
{
    public class CreateModel : PageModel
    {
		const string BASE_URL = "https://localhost:7259";
		string RELATIVE_PATH_URL = $"/api/Members";
		const string HTTP_GET = "GET";
		const string HTTP_PUT = "PUT";
        const string HTTP_POST = "POST";
        const string ROLE_ADMIN = "admin";
		const string ROLE_MEMBER = "member";

		private readonly IClientsRequestService<CreateCategoryRequest> requestService;

		public CreateModel(IClientsRequestService<CreateCategoryRequest> requestService)
		{
			this.requestService = requestService;
		}

		public void OnGet()
        {
        }

        [BindProperty] public CreateCategoryRequest category { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
			if (!ModelState.IsValid)
			{
				return Page();
			}

			CommonClientsRequest<CreateCategoryRequest> posterRequest = new CommonClientsRequest<CreateCategoryRequest>()
			{
				httpBaseUrl = BASE_URL,
				httpRelativePath = $"{RELATIVE_PATH_URL}",
				httpMethod = HTTP_POST,
				Data = category
			};

			HttpResponseMessage createResponse = await requestService.HandleClientsRequest<CommonClientsRequest<CreateCategoryRequest>, HttpResponseMessage, CreateCategoryRequest>(posterRequest);
			return RedirectToPage("./Index");
        }
    }
}
