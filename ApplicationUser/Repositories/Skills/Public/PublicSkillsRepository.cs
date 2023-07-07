using Eravol.UserWebApi.Data;
using Eravol.UserWebApi.Data.Models;

namespace Eravol.WebApi.Repositories.Skills.Public
{
    public class PublicSkillsRepository : IPublicSkillRepository
    {
        #region Dependency Injection Services
        private readonly EravolUserWebApiContext context;
        #endregion

        #region Construtor
        public PublicSkillsRepository(EravolUserWebApiContext context)
        {
            this.context = context;
        }
        #endregion


        /// <summary>
        /// Get All public skills in database
        /// </summary>
        /// <returns>Public skills</returns>
        /// <exception cref="Exception"></exception>
        public List<Skill> GetAllPublicSkills()
        {
			try
			{
				List<Skill>? list = context.Skills
                    .Where(x => x.isPublic != false)
                    .ToList();
                return list;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
        }
    }
}
