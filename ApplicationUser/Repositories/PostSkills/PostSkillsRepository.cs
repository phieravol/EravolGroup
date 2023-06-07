using Eravol.UserWebApi.Data;
using Eravol.WebApi.Data.Models;
using Eravol.WebApi.ViewModels.PostSkillRequires;
using Microsoft.EntityFrameworkCore;

namespace Eravol.WebApi.Repositories.PostSkills
{
    public class PostSkillsRepository : IPostSkillsRepository
    {
        private readonly EravolUserWebApiContext context;

        public PostSkillsRepository(EravolUserWebApiContext context)
        {
            this.context = context;
        }

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
    }
}
