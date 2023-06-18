using Eravol.UserWebApi.Data;
using Eravol.WebApi.Data.Models;
using Eravol.WebApi.ViewModels.PostSkillRequires;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Eravol.WebApi.Repositories.PostSkills
{
    public class PostSkillsRepository : IPostSkillsRepository
    {
        private readonly EravolUserWebApiContext context;

        public PostSkillsRepository(EravolUserWebApiContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Create Multiple SkillRequires in Db by List of PostSkillRequired
        /// </summary>
        /// <param name="skillRequires"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task CreateSkillsRequireAsync(List<PostSkillRequired> skillRequires)
        {
            try
            {
                context.PostSkilRequires.AddRangeAsync(skillRequires);
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            };
        }

        /// <summary>
        /// Get PostSkillRequire from DB by Id
        /// </summary>
        /// <param name="skillRequireId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<PostSkillRequired> GetSkillRequireById(int? skillRequireId)
        {
            try
            {
                PostSkillRequired? postSkill = await context.PostSkilRequires.FindAsync(skillRequireId);
                return postSkill;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            };
        }

        /// <summary>
        /// UpdateServiceRequest PostSkillRequire by current object
        /// </summary>
        /// <param name="skillRequire"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task UpdateSkillRequire(PostSkillRequired skillRequire)
        {
            try
            {

                context.Entry<PostSkillRequired>(skillRequire).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            };
        }
    }
}
