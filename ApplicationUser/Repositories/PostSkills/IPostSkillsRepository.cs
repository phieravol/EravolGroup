using Eravol.WebApi.Data.Models;
using Eravol.WebApi.ViewModels.PostSkillRequires;

namespace Eravol.WebApi.Repositories.PostSkills
{
    public interface IPostSkillsRepository
    {
        Task CreateSkillsRequireAsync(List<PostSkillRequired> skillRequires);
        Task<PostSkillRequired> GetSkillRequireById(int? skillRequireId);
        Task UpdateSkillRequire(PostSkillRequired skillRequire);
    }
}
