using Eravol.UserWebApi.Data;
using Eravol.UserWebApi.Data.Models;
using Eravol.WebApi.Data.Models;
using Eravol.WebApi.ViewModels.UserSkills;
using Microsoft.EntityFrameworkCore;

namespace Eravol.WebApi.Repositories.UserSkills
{
    public class PublicUserSkillRepository : IPublicUserSkillRepository
    {
        private readonly EravolUserWebApiContext context;

        public PublicUserSkillRepository(EravolUserWebApiContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Create new UserSkill to database
        /// </summary>
        /// <param name="userSkill">An UserSkill object</param>
        /// <returns>No return</returns>
        /// <exception cref="Exception"></exception>
        public async Task AddUserSkillToDb(UserSkill userSkill)
        {
            try
            {
                context.UserSkills.Add(userSkill);
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Delete UserSkill
        /// </summary>
        /// <param name="userSkill"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task DeleteUserSkill(UserSkill userSkill)
        {
            try
            {
                context.UserSkills.Remove(userSkill);
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Get UserSkill by UserId
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns></returns>
        /// <exception cref="Exception">List UserSkillViewModel from database</exception>
        public async Task<List<UserSkillViewModel>?> GetMyUserSkillsByUserId(Guid userId)
        {
            try
            {
                var query = from us in context.UserSkills
                            join s in context.Skills on us.SkillId equals s.Id
                            select new { us, s };
                List<UserSkillViewModel>? userSkills = await query.Select(x => new UserSkillViewModel()
                {
                    UserSkillId = x.us.UserSkillId,
                    SkillId = x.s.Id,
                    UserSkillName = x.s.SkillName,
                    IsVerified = x.us.IsVerified,
                    Score= x.us.Score,
                    UserId= userId
                }).ToListAsync();

                return userSkills;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Get UserSkill by Skill ID and User ID
        /// </summary>
        /// <param name="skillId">Skill Id</param>
        /// <param name="userId">User Id</param>
        /// <returns>An UserSkill Object from database</returns>
        /// <exception cref="Exception"></exception>
        public UserSkill GetSpecificUserSkill(int skillId, Guid userId)
        {
            try
            {
                UserSkill? userSkill = context.UserSkills.FirstOrDefault(x => x.SkillId == skillId && x.UserId == userId);
                return userSkill;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Get UserSkill By UserSkillId
        /// </summary>
        /// <param name="userSkillId">Current UserSkillId</param>
        /// <returns>UserSkill object</returns>
        /// <exception cref="Exception"></exception>
        public async Task<UserSkill?> GetUserSkillById(int userSkillId)
        {
            try
            {
                UserSkill? userSkill = await context.UserSkills.FirstOrDefaultAsync(x => x.UserSkillId == userSkillId);
                return userSkill;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Update UserSkill score
        /// </summary>
        /// <param name="userSkill">Score</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task UpdateUserSkillScore(UserSkill userSkill)
        {
            try
            {
                context.UserSkills.Update(userSkill);
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
