using Eravol.WebApi.Data.Models;
using Eravol.WebApi.ViewModels.Services.Freelancers;
using Eravol.WebApi.ViewModels.Services.Public;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Policy;

namespace Eravol.UIClient.Repositories.Services.Public
{
    public class PublicServices : IPublicServices
    {
        const string BASE_URL = "https://localhost:7259";
        string SERVICE_PATH_URL = $"api/ServicesPublic/";

        private readonly IHttpClientFactory clientFactory;

        public PublicServices(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        public async Task<ServiceViewModel?> GetServiceDetailAsync(string serviceCode)
        {

            string url = SERVICE_PATH_URL + serviceCode;
            var client = clientFactory.CreateClient();

            client.BaseAddress = new Uri(BASE_URL);
            HttpResponseMessage response = await client.GetAsync(url);

            string dataResponse = await response.Content.ReadAsStringAsync();
            ServiceViewModel? service = JsonConvert.DeserializeObject<ServiceViewModel>(dataResponse);
            return service;
        }
    }
}
