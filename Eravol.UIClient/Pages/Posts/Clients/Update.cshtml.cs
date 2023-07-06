using Eravol.UIClient.Repositories.Posts.Clients;
using Eravol.UserWebApi.Data.Models;
using Eravol.WebApi.Data.Models;
using Eravol.WebApi.ViewModels.PostSkillRequires;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eravol.UIClient.Pages.Posts.Clients
{
    public class UpdateModel : PageModel
    {
        const string BASE_URL = "https://localhost:7259";
        string RELATIVE_PATH_URL = $"/api/Posts";
        string CATEGORY_PATH_URL = $"/api/PublicCategories";
        string SKILL_PATH_URL = $"/api/skills/Skills";
        string CLIENT_POST_PATH = $"/api/Posts/";
        string POST_SKILL_REQUIRE = $"/api/PostSkillRequire/";

        private readonly IClientPostsRepository postsRepository;

        public UpdateModel(IClientPostsRepository postsRepository)
        {
            this.postsRepository = postsRepository;
        }

        [BindProperty(SupportsGet = true)] public List<Category> Categories { get; set; }
        [BindProperty(SupportsGet = true)] public List<PostSkillRequired> PostSkillRequireds { get; set; }
        [BindProperty(SupportsGet = true)] public List<Skill> Skills { get; set; }
        [BindProperty(SupportsGet = true)] public string? token { get; set; }
        [BindProperty(SupportsGet = true)] public int? PostId { get; set; }
        [BindProperty(SupportsGet = true)] public Post? CurrentPost { get; set; }
        [BindProperty(SupportsGet = true)] public List<PostSkillRequireViewModel>? SkillsRequire { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            if (PostId == null)
            {
                return NotFound();
            }

            //get token by session
            token = HttpContext.Session.GetString("AuthToken");
            if (token == null)
            {
                return RedirectToPage("/Forbidden");
            }
            //Get all category from API
            Categories = await postsRepository.GetAllCategoriesFromApiAsync(CATEGORY_PATH_URL, token);
            //Get all PostSkillRequired
            Skills = await postsRepository.GetAllPostSkillRequiredFromApiAsync(SKILL_PATH_URL, token);
            //Get current post in database
            CurrentPost = await postsRepository.GetCurrentPostById(CLIENT_POST_PATH, token, PostId);
            //Get current post skill required
            SkillsRequire = await postsRepository.GetPostSkillsRequireAsync(POST_SKILL_REQUIRE, token, PostId);
            return Page();
        }
    }
}
