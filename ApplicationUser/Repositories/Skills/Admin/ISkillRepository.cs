using Eravol.UserWebApi.Data.Models;
using Eravol.UserWebApi.ViewModels.Skills;
using Eravol.WebApi.Data.Models;
using Eravol.WebApi.ViewModels.Base;
using Eravol.WebApi.ViewModels.PostStatuses;

namespace Eravol.WebApi.Repositories.Skills.Admin
{
    public interface ISkillRepository
    {
        Task CreateSkillAsync(SkillViewModel skill);
        Task DeleteSkillAsync(Skill skill);
        Task<Skill> GetSkillByIdAsync(int? skillId);
        Task<List<Skill>> GetSkillSearchPaging(PagingRequestBase<Skill> request);
        Task UpdateSkillAsync(Skill skill);
    }
}
