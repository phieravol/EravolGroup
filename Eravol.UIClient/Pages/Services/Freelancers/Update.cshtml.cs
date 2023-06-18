using Eravol.UIClient.Repositories.Services.Freelancers;
using Eravol.WebApi.Data.Models;
using Eravol.WebApi.ViewModels.Services.Freelancers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using NuGet.Common;
using System.Drawing;
using System.Net.Http.Headers;

namespace Eravol.UIClient.Pages.Services.Freelancers
{
	public class UpdateModel : PageModel
	{
		const string BASE_URL = "https://localhost:7259";
		string SERVICE_PATH_URL = $"/api/Services";
		string CATEGORY_PATH_URL = $"/api/PublicCategories";
		string SERVICE_STATUS_URL = $"/api/ServiceStatuses";


		private readonly IHttpClientFactory clientFactory;
		private readonly IFreelancerServices freelancerServices;

		public UpdateModel(IHttpClientFactory clientFactory, IFreelancerServices freelancerServices)
		{
			this.clientFactory = clientFactory;
			this.freelancerServices = freelancerServices;
		}

		[BindProperty(SupportsGet = true)] public Service? Service { get; set; }
		[BindProperty(SupportsGet = true)] public string? serviceId { get; set; }
		[BindProperty(SupportsGet = true)] public List<Category> Categories { get; set; }
		[BindProperty(SupportsGet = true)] public List<ServiceStatus> ServiceStatuses { get; set; }
		[BindProperty(SupportsGet = true)] public string? token { get; set; }
		[BindProperty(SupportsGet = true)] public List<ServiceImage>? serviceImages { get; set; }
		[BindProperty(SupportsGet = true)] public ServiceImage? thumbnail { get; set; }
		[BindProperty] public CreateServicesRequest CreateServicesRequest { get; set; }
		

		public async Task<IActionResult> OnGetAsync()
        {
			if (serviceId == null)
			{
				return NotFound();
			}

			//get token by session
			token = HttpContext.Session.GetString("AuthToken");

			if (token == null)
			{
				return RedirectToPage("/Forbidden");
			}

			//Get all categories from api
			Categories = await freelancerServices.GetAllCategoriesFromApiAsync(CATEGORY_PATH_URL, token);

			//Get all ServiceStatuses from api
			ServiceStatuses = await freelancerServices.GetAllServiceStatusApiAsync(SERVICE_STATUS_URL, token);

			//Get Current service information
			Service = await freelancerServices.getCurrentServiceInfo(serviceId, token);
			if (Service == null)
			{
				return NotFound($"Can't find service by code = {serviceId}");
			}

			//Get Service Images by Service Code
			serviceImages = await freelancerServices.GetServiceImagesBycode(serviceId);

			//Get Service thumbnail by service code
			thumbnail = await freelancerServices.GetServiceThumbnail(serviceId);

			return Page();
		}

	}
}
