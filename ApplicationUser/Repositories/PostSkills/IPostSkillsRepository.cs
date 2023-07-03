using Eravol.WebApi.Data.Models;
using Eravol.WebApi.ViewModels.PostSkillRequires;

namespace Eravol.WebApi.Repositories.PostSkills
{
    public interface IPostSkillsRepository
    {
        Task CreateSkillsRequireAsync(List<PostSkillRequired> skillRequires);
        Task CreateSpecifySkillRequireAsync(PostSkillRequired skillRequired);
        Task DeleteSkillRequire(PostSkillRequired skillRequired);
        Task<PostSkillRequired> GetSkillRequireById(int? skillRequireId);
        Task<List<PostSkillRequireViewModel>> GetSkillRequireByPostId(int? postId);
        Task<PostSkillRequired?> GetSpecificSkillRequire(int? skillRequireId, int? postId);
        Task UpdateSkillRequire(PostSkillRequired skillRequire);
    }
}
