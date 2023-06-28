using Eravol.UIClient.Repositories.General;
using Eravol.UserWebApi.Data.Models;
using Eravol.WebApi.Data.Models;
using Eravol.WebApi.ViewModels.Categories;
using Eravol.WebApi.ViewModels.Posts.Clients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Eravol.UIClient.Pages.Posts.Clients
{
    public class CreateModel : PageModel
    {
        const string BASE_URL = "https://localhost:7259";
        string RELATIVE_PATH_URL = $"/api/Posts";
        string CATEGORY_PATH_URL = $"/api/PublicCategories";
        string SKILL_PATH_URL = $"/api/skills/Skills";

        private readonly IHttpClientFactory clientFactory;

        public CreateModel(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }
        [BindProperty(SupportsGet = true)] public List<Category> Categories { get; set; }
        [BindProperty(SupportsGet = true)] public List<PostSkillRequired> PostSkillRequireds { get; set; }
        [BindProperty(SupportsGet = true)] public List<Skill> Skills { get; set; }
        [BindProperty(SupportsGet = true)] public string? token { get; set; }
        [BindProperty] public CreatePostRequest CreatePostRequest { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            //get token by session
            token = HttpContext.Session.GetString("AuthToken");
            if (token == null)
            {
                return RedirectToPage("/Forbidden");
            }
            //Get all category from API
            Categories = await GetAllCategoriesFromApiAsync(CATEGORY_PATH_URL, token);
            //Get all PostSkillRequired
            Skills = await GetAllPostSkillRequiredFromApiAsync(SKILL_PATH_URL, token);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            return Page();
        }
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
        private async Task<List<Skill>> GetAllPostSkillRequiredFromApiAsync(string RELATIVE_URL, string token)
        {
            //Create new client to send request to api
            var client = clientFactory.CreateClient();
            client.BaseAddress = new Uri(BASE_URL);

            //Set authorization header for request
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //Get response from api
            HttpResponseMessage response = await client.GetAsync(RELATIVE_URL);
            string dataResponse = await response.Content.ReadAsStringAsync();
            List<Skill>? skills = JsonConvert.DeserializeObject<List<Skill>>(dataResponse);
            return skills;
        }
    }
}
