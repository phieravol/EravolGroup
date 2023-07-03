using Eravol.UserWebApi.Data.Models;
using Eravol.WebApi.Data.Models;
using Eravol.WebApi.ViewModels.PostSkillRequires;

namespace Eravol.UIClient.Repositories.Posts.Clients
{
    public interface IClientPostsRepository
    {
        Task<List<Category>> GetAllCategoriesFromApiAsync(string CATEGORY_PATH_URL, string token);
        Task<List<Skill>> GetAllPostSkillRequiredFromApiAsync(string SKILL_PATH_URL, string token);
        Task<Post?> GetCurrentPostById(string CLIENT_POST_PATH, string? token, int? postId);
        Task<List<PostSkillRequireViewModel>?> GetPostSkillsRequireAsync(string POST_SKILL_REQUIRE, string token, int? postId);
    }
}
