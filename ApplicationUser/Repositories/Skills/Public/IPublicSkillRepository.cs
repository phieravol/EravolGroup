using Eravol.UserWebApi.Data.Models;

namespace Eravol.WebApi.Repositories.Skills.Public
{
    public interface IPublicSkillRepository
    {
        List<Skill> GetAllPublicSkills();
    }
}
