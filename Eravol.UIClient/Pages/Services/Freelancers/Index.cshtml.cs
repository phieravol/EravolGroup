using Eravol.UIClient.Repositories.Services.Freelancers;
using Eravol.WebApi.Data.Models;
using Eravol.WebApi.Repositories.ServiceImages.Freelancers;
using Eravol.WebApi.ViewModels.Base;
using Eravol.WebApi.ViewModels.Services.Freelancers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using NuGet.Common;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Claims;

namespace Eravol.UIClient.Pages.Services.Freelancers
{
    public class IndexModel : PageModel
    {
		const string BASE_URL = "https://localhost:7259";
		string SERVICE_PATH_URL = $"/api/Services";

		private readonly IHttpClientFactory clientFactory;
		private readonly IFreelancerServices freelancerServices;

		public IndexModel(
			IHttpClientFactory clientFactory,
			IFreelancerServices freelancerServices
		)
		{
			this.clientFactory = clientFactory;
			this.freelancerServices = freelancerServices;
		}

        [BindProperty(SupportsGet = true)] public ServicePagingRequest? PagingRequest { get; set; }

		[Authorize]
		public async Task<IActionResult> OnGetAsync()
        {
			//Get current UserId by claims
			string UserIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			PagingRequest.UserIdStr = UserIdStr;

			// config endpoint url
			string searchTermStr = (PagingRequest.SearchTerm != null) ? $"SearchTerm={PagingRequest.SearchTerm}" : "";
			string currentPageStr = $"CurrentPage={PagingRequest.CurrentPage}";
			string pageSizeStr = $"PageSize={PagingRequest.PageSize}";
			string hasNextStr = $"HasNext={PagingRequest.HasNext}";
			string hasPreStr = $"HasPrevious={PagingRequest.HasPrevious}";
			string userIdStr = $"UserIdStr={PagingRequest.UserIdStr}";
			string Url = $"{SERVICE_PATH_URL}?{searchTermStr}&{currentPageStr}&{pageSizeStr}&{hasNextStr}&{hasPreStr}&{userIdStr}";

			//Create new client to send request to api
			var client = clientFactory.CreateClient();

			//get token by session
			string? token = HttpContext.Session.GetString("AuthToken");

			if (token==null)
			{
				return RedirectToPage("/Forbidden");
			}

			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			client.BaseAddress = new Uri(BASE_URL);
			HttpResponseMessage response = await client.GetAsync(Url);

			string dataResponse = await response.Content.ReadAsStringAsync();
			PagingRequest = JsonConvert.DeserializeObject<ServicePagingRequest>(dataResponse);
			return Page();
        }
    }
}
