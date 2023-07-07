using Eravol.UserWebApi.Data;
using Eravol.UserWebApi.Data.Models;
using Eravol.UserWebApi.ViewModels.Skills;
using Eravol.WebApi.Data.Models;
using Eravol.WebApi.ViewModels.Base;
using Microsoft.EntityFrameworkCore;

namespace Eravol.WebApi.Repositories.Skills.Admin
{
    public class SkillRepository : ISkillRepository
    {
        private readonly EravolUserWebApiContext context;

        public SkillRepository(EravolUserWebApiContext context)
        {
            this.context = context;
        }

        public async Task CreateSkillAsync(SkillViewModel skill)
        {
            try
            {
                Skill skills = new Skill()
                {
                    SkillName = skill.SkillName,
                    isPublic = skill.isPublic
                };
                context.Skills.Add(skills);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteSkillAsync(Skill skill)
        {
            try
            {
                context.Skills.Remove(skill);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Skill> GetSkillByIdAsync(int? skillId)
        {
            try
            {
                Skill? skill = await context.Skills.FindAsync(skillId);
                return skill;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Skill>> GetSkillSearchPaging(PagingRequestBase<Skill> request)
        {
            try
            {
                IQueryable<Skill> query = context.Skills/*.Where(x => x.isPublic.Value == true)*/;

                if (!string.IsNullOrEmpty(request.SearchTerm))
                {
                    query = query.Where(x => x.SkillName.Contains(request.SearchTerm));
                }
                request.Items = await query.ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return request.Items;
        }

        public async Task UpdateSkillAsync(Skill skill)
        {
            try
            {
                context.Entry(skill).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
