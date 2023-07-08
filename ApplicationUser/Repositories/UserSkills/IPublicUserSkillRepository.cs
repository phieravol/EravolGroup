using Eravol.WebApi.Data.Models;
using Eravol.WebApi.ViewModels.UserSkills;

namespace Eravol.WebApi.Repositories.UserSkills
{
    public interface IPublicUserSkillRepository
    {
        Task AddUserSkillToDb(UserSkill userSkill);
        Task DeleteUserSkill(UserSkill userSkill);
        Task<List<UserSkillViewModel>?> GetMyUserSkillsByUserId(Guid userId);
        UserSkill GetSpecificUserSkill(int skillId, Guid userId);
        Task<UserSkill?> GetUserSkillById(int userSkillId);
        Task UpdateUserSkillScore(UserSkill userSkill);
    }
}
