using Eravol.WebApi.Data.Models;
using Eravol.WebApi.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

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

		private readonly IHttpClientFactory clientFactory;

		public UpdateModel(IHttpClientFactory clientFactory)
		{
			this.clientFactory = clientFactory;
		}

		[BindProperty(SupportsGet = true)] public int? CategoryId { get; set; }
		[BindProperty] public Category? category { get; set; }
		[BindProperty] public IFormFile? categoryImg { get; set; }

		public async Task<IActionResult> OnGetAsync()
        {
			var client = clientFactory.CreateClient();
			client.BaseAddress = new Uri(BASE_URL);
			string url = $"{RELATIVE_PATH_URL}/CategoryId?CategoryId={CategoryId}";
			HttpResponseMessage response = await client.GetAsync(url);
			string dataResponse = await response.Content.ReadAsStringAsync();
			category = JsonConvert.DeserializeObject<Category>(dataResponse);

			return Page();
        }

		public async Task<IActionResult> OnPostAsync()
		{
			var client = clientFactory.CreateClient();
			client.BaseAddress = new Uri(BASE_URL);
			string url = $"{RELATIVE_PATH_URL}/{category.CategoryId}";

			UpdateCategoryRequest? updateCategoryRequest = new UpdateCategoryRequest()
			{
				CategoryId = category.CategoryId,
				CategoryName = category.CategoryName,
				CategoryDesc = category.CategoryDesc,
				CategoryLevel = 1,
				isCategoryActive = category.isCategoryActive,
				CategoryImage = categoryImg
			};

			var formData = new MultipartFormDataContent
			{
				{ new StreamContent(updateCategoryRequest.CategoryImage.OpenReadStream()), "CategoryImage", categoryImg.FileName },
				{ new StringContent(updateCategoryRequest.CategoryName), "CategoryName" },
				{ new StringContent(updateCategoryRequest.CategoryId.ToString()), "CategoryId" },
				{ new StringContent(updateCategoryRequest.isCategoryActive.ToString()), "isCategoryActive" },
				{ new StringContent(updateCategoryRequest.CategoryDesc), "CategoryDesc" }
			};

			var response = await client.PutAsync(url, formData);

			return RedirectToPage("./Index");
		}
    }
}
