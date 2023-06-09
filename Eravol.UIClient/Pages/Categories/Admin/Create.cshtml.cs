using Eravol.UIClient.Repositories.General;
using Eravol.UIClient.ViewModels.General;
using Eravol.WebApi.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eravol.UIClient.Pages.Categories.Admin
{
	public class CreateModel : PageModel
    {
		const string BASE_URL = "https://localhost:7259";
		string RELATIVE_PATH_URL = $"/api/Admin/Categories";
		const string HTTP_GET = "GET";
		const string HTTP_PUT = "PUT";
        const string HTTP_POST = "POST";
        const string ROLE_ADMIN = "admin";
		const string ROLE_MEMBER = "member";

		private readonly IClientsRequestService<CreateCategoryRequest> requestService;
		private readonly IHttpClientFactory clientFactory;

		public CreateModel(
			IClientsRequestService<CreateCategoryRequest> requestService,
			IHttpClientFactory clientFactory
		)
		{
			this.requestService = requestService;
			this.clientFactory = clientFactory;
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

			var client = clientFactory.CreateClient();
			client.BaseAddress = new Uri(posterRequest.httpBaseUrl);

			var formData = new MultipartFormDataContent
			{
				{ new StreamContent(category.CategoryImage.OpenReadStream()), "CategoryImage", category.CategoryImage.FileName },
				{ new StringContent(category.CategoryName), "CategoryName" },
				{ new StringContent(category.isCategoryActive.ToString()), "isCategoryActive" },
				{ new StringContent(category.CategoryDesc), "CategoryDesc" }
			};

			var response = await client.PostAsync(posterRequest.httpRelativePath, formData);
			return RedirectToPage("./Index");
        }
    }
}
