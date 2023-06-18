using Eravol.WebApi.Data.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Common;
using System.Net.Http.Headers;

namespace Eravol.UIClient.Repositories.Services.Freelancers
{

	public class FreelancerServices : IFreelancerServices
	{
		const string BASE_URL = "https://localhost:7259";
		string SERVICE_PATH_URL = $"/api/Services";
		string CATEGORY_PATH_URL = $"/api/PublicCategories";
		string SERVICE_STATUS_URL = $"/api/ServiceStatuses";
		string SERVICE_IMAGES_URL = $"/api/ServiceImages";
		string SERVICE_THUMBNAIL_URL = $"/api/ServiceImages/thumbnail";

		private readonly IHttpClientFactory clientFactory;

		public FreelancerServices(IHttpClientFactory clientFactory)
		{
			this.clientFactory = clientFactory;
		}

		/// <summary>
		/// Get Category From backend api Controller/PublicCategoryController
		/// </summary>
		/// <param name="RELATIVE_URL"></param>
		/// <param name="token"></param>
		/// <returns></returns>
		public async Task<List<Category>> GetAllCategoriesFromApiAsync(string RELATIVE_URL, string token)
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

		/// <summary>
		/// Get All ServiceStatuses from api Controllers/ServiceStatuses/ServiceStatusesController
		/// </summary>
		/// <param name="RELATIVE_URL"></param>
		/// <param name="token"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<List<ServiceStatus>> GetAllServiceStatusApiAsync(string RELATIVE_URL, string token)
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
		/// Get current Service by service code
		/// </summary>
		/// <param name="serviceId"></param>
		/// <param name="token"></param>
		/// <returns></returns>
		public async Task<Service?> getCurrentServiceInfo(string serviceId, string token)
		{
			//Create new client to send request to api
			var client = clientFactory.CreateClient();

			//Set authorization header for request
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			client.BaseAddress = new Uri(BASE_URL);

			//Get response from api
			string serviceUrl = $"{SERVICE_PATH_URL}/{serviceId}";
			HttpResponseMessage response = await client.GetAsync(serviceUrl);
			string dataResponse = await response.Content.ReadAsStringAsync();
			Service? service = JsonConvert.DeserializeObject<Service>(dataResponse);
			return service;
		}

		/// <summary>
		/// Get service images by service code
		/// </summary>
		/// <param name="serviceId"></param>
		/// <returns></returns>
		public async Task<List<ServiceImage>?> GetServiceImagesBycode(string serviceId)
		{
			//Create new client to send request to api
			var client = clientFactory.CreateClient();

			//Set authorization header for request
			client.BaseAddress = new Uri(BASE_URL);

			//Get response from api
			string serviceUrl = $"{SERVICE_IMAGES_URL}/{serviceId}";
			HttpResponseMessage response = await client.GetAsync(serviceUrl);
			string dataResponse = await response.Content.ReadAsStringAsync();
			List<ServiceImage>? service = JsonConvert.DeserializeObject<List<ServiceImage>>(dataResponse);
			return service;
		}

		/// <summary>
		/// Get service thumbnail by service code
		/// </summary>
		/// <param name="serviceId"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<ServiceImage?> GetServiceThumbnail(string serviceId)
		{
			//Create new client to send request to api
			var client = clientFactory.CreateClient();

			//Set authorization header for request
			client.BaseAddress = new Uri(BASE_URL);

			//Get response from api
			string serviceUrl = $"{SERVICE_THUMBNAIL_URL}/{serviceId}";
			HttpResponseMessage response = await client.GetAsync(serviceUrl);
			string dataResponse = await response.Content.ReadAsStringAsync();
			ServiceImage? service = JsonConvert.DeserializeObject<ServiceImage>(dataResponse);
			return service;
		}
	}
}
