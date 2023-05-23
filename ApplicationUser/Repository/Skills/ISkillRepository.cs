using Eravol.UserWebApi.Data.Models;
using Eravol.UserWebApi.ViewModels.Skills;

namespace Eravol.UserWebApi.Repository.Skills
{
    public interface ISkillRepository
    {
        void CreateUserSkill(Skill request);
        Skill getSkillById(int? skillId);
        List<Skill> GetSkillsByUsername(string username);
        Task RemoveSkillAsync(Skill skill);
        Task UpdateSkillAsync(Skill skill);
    }
}
