using Eravol.UserWebApi.Data.Models;
using Eravol.WebApi.Data.Models;
using Eravol.WebApi.ViewModels.PostSkillRequires;
using Newtonsoft.Json;
using NuGet.Common;
using System.Net.Http.Headers;

namespace Eravol.UIClient.Repositories.Posts.Clients
{
    public class ClientPostsRepository: IClientPostsRepository
    {
        const string BASE_URL = "https://localhost:7259";
        string RELATIVE_PATH_URL = $"/api/Posts";
        string CATEGORY_PATH_URL = $"/api/PublicCategories";
        string SKILL_PATH_URL = $"/api/skills/Skills";

        private readonly IHttpClientFactory clientFactory;

        public ClientPostsRepository(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        public async Task<List<Category>> GetAllCategoriesFromApiAsync(string CATEGORY_PATH_URL, string token)
        {
            //Create new client to send request to api
            var client = clientFactory.CreateClient();
            client.BaseAddress = new Uri(BASE_URL);

            //Set authorization header for request
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //Get response from api
            HttpResponseMessage response = await client.GetAsync(CATEGORY_PATH_URL);
            string dataResponse = await response.Content.ReadAsStringAsync();
            List<Category>? categories = JsonConvert.DeserializeObject<List<Category>>(dataResponse);
            return categories;
        }

        public async Task<List<Skill>> GetAllPostSkillRequiredFromApiAsync(string SKILL_PATH_URL, string token)
        {
            //Create new client to send request to api
            var client = clientFactory.CreateClient();
            client.BaseAddress = new Uri(BASE_URL);

            //Set authorization header for request
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //Get response from api
            HttpResponseMessage response = await client.GetAsync(SKILL_PATH_URL);
            string dataResponse = await response.Content.ReadAsStringAsync();
            List<Skill>? skills = JsonConvert.DeserializeObject<List<Skill>>(dataResponse);
            return skills;
        }

        public async Task<Post?> GetCurrentPostById(string CLIENT_POST_PATH, string? token, int? postId)
        {
            //Create new client to send request to api
            var client = clientFactory.CreateClient();
            client.BaseAddress = new Uri(BASE_URL);

            //Set authorization header for request
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //Get response from api
            string URL = CLIENT_POST_PATH + postId.ToString(); 
            HttpResponseMessage response = await client.GetAsync(URL);
            string dataResponse = await response.Content.ReadAsStringAsync();
            Post? Post = JsonConvert.DeserializeObject<Post>(dataResponse);
            return Post;
        }

        public async Task<List<PostSkillRequireViewModel>?> GetPostSkillsRequireAsync(string POST_SKILL_REQUIRE, string token, int? postId)
        {
            //Create new client to send request to api
            var client = clientFactory.CreateClient();
            client.BaseAddress = new Uri(BASE_URL);

            //Set authorization header for request
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //Get response from api
            string URL = POST_SKILL_REQUIRE + postId.ToString();
            HttpResponseMessage response = await client.GetAsync(URL);
            string dataResponse = await response.Content.ReadAsStringAsync();
            List<PostSkillRequireViewModel>? Skills = JsonConvert.DeserializeObject<List<PostSkillRequireViewModel>>(dataResponse);
            return Skills;
        }
    }
}
