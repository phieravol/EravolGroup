using Eravol.UserWebApi.Data;
using Eravol.UserWebApi.Data.Models;
using Eravol.UserWebApi.ViewModels.Skills;
using Microsoft.EntityFrameworkCore;

namespace Eravol.UserWebApi.Repository.Skills
{
    public class SkillRepository : ISkillRepository
    {
        private readonly EravolUserWebApiContext context;

        public SkillRepository(EravolUserWebApiContext context)
        {
            this.context = context;
        }

        public void CreateUserSkill(Skill request)
        {
            context.Skills.Add(request);
            context.SaveChanges();
        }

        public List<Skill> GetPublicSkills()
        {
            return context.Skills.ToList();
        }

        public Skill getSkillById(int? skillId)
        {
            return context.Skills.Find(skillId);
        }

        /// <summary>
        /// Get all Skill of user by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public List<Skill> GetSkillsByUsername(string username)
        {
            var query = from skill in context.Skills
                        //join user in context.AppUsers on skill.UserId equals user.Id
                        //where user.UserName == username
                        select skill;
            return query.ToList();
        }

        public async Task RemoveSkillAsync(Skill skill)
        {
            context.Skills.Remove(skill);
            await context.SaveChangesAsync();
        }

        public async Task UpdateSkillAsync(Skill skill)
        {
            context.Entry(skill).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}
