using Eravol.WebApi.Data.Models;
using Eravol.WebApi.ViewModels.Services.Freelancers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Eravol.UIClient.Pages.Services.Freelancers
{
    public class CreateModel : PageModel
    {
		const string BASE_URL = "https://localhost:7259";
		string SERVICE_PATH_URL = $"/api/Services";
		string CATEGORY_PATH_URL = $"/api/PublicCategories";
		string SERVICE_STATUS_URL = $"/api/ServiceStatuses";

		private readonly IHttpClientFactory clientFactory;

		public CreateModel(IHttpClientFactory clientFactory)
		{
			this.clientFactory = clientFactory;
		}

		[BindProperty(SupportsGet = true)] public List<Category> Categories { get; set; }
		[BindProperty(SupportsGet = true)] public List<ServiceStatus> ServiceStatuses { get; set; }
		[BindProperty(SupportsGet = true)] public string? token { get; set; }
		[BindProperty] public CreateServicesRequest CreateServicesRequest { get; set; }



		/// <summary>
		/// OnGetAsync to get category and ServiceStatuses
		/// </summary>
		/// <returns></returns>
		public async Task<IActionResult> OnGetAsync()
		{
			//get token by session
			token = HttpContext.Session.GetString("AuthToken");

			if (token == null)
			{
				return RedirectToPage("/Forbidden");
			}

			//Get all categories from api
			Categories = await GetAllCategoriesFromApiAsync(CATEGORY_PATH_URL, token);

			//Get all ServiceStatuses from api
			ServiceStatuses = await GetAllServiceStatusApiAsync(SERVICE_STATUS_URL, token);

			return Page();
		}

		/// <summary>
		/// OnPostAsync To Create Service
		/// </summary>
		/// <returns></returns>
		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}
			return Page();
		}


		/// <summary>
		/// Get All ServiceStatuses from api Controllers/ServiceStatuses/ServiceStatusesController
		/// </summary>
		/// <param name="RELATIVE_URL"></param>
		/// <param name="token"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		private async Task<List<ServiceStatus>> GetAllServiceStatusApiAsync(string RELATIVE_URL, string token)
		{
			//Create new client to send request to api
			var client = clientFactory.CreateClient();

			//Set authorization header for request
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			client.BaseAddress = new Uri(BASE_URL);

			//Get response from api
			HttpResponseMessage response = await client.GetAsync(RELATIVE_URL);
			string dataResponse = await response.Content.ReadAsStringAsync();
			List<ServiceStatus>? serviceStatuses = JsonConvert.DeserializeObject<List<ServiceStatus>>(dataResponse);
			return serviceStatuses;
		}


		/// <summary>
		/// Get Category From backend api Controller/PublicCategoryController
		/// </summary>
		/// <param name="RELATIVE_URL"></param>
		/// <param name="token"></param>
		/// <returns></returns>
		private async Task<List<Category>> GetAllCategoriesFromApiAsync(string RELATIVE_URL, string token)
		{
			//Create new client to send request to api
			var client = clientFactory.CreateClient();
			client.BaseAddress = new Uri(BASE_URL);

			//Set authorization header for request
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			//Get response from api
			HttpResponseMessage response = await client.GetAsync(RELATIVE_URL);
			string dataResponse = await response.Content.ReadAsStringAsync();
			List<Category>? categories = JsonConvert.DeserializeObject<List<Category>>(dataResponse);
			return categories;
		}
	}
}
