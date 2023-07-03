using Eravol.WebApi.Data.Models;
using Newtonsoft.Json;
using NuGet.Common;
using System.Net.Http.Headers;

namespace Eravol.UIClient.Repositories.Categories
{
    public class PublicCategoryService : IPublicCategoryService
    {
        #region Constants
        private const string BASE_URL = "https://localhost:7259";
        private const string CATEGORY_PUBLIC_URL = "api/PublicCategories";
        #endregion

        #region Dependencies injection services
        private readonly IHttpClientFactory clientFactory;
        #endregion

        #region Constructor
        public PublicCategoryService(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }
        #endregion

        public async Task<List<Category>> GetAllPublicCategory()
        {
            //Create new client to send request to api
            var client = clientFactory.CreateClient();
            client.BaseAddress = new Uri(BASE_URL);

            //Get response from api
            HttpResponseMessage response = await client.GetAsync(CATEGORY_PUBLIC_URL);
            string dataResponse = await response.Content.ReadAsStringAsync();
            List<Category>? categories = JsonConvert.DeserializeObject<List<Category>>(dataResponse);
            return categories;
        }
    }
}
