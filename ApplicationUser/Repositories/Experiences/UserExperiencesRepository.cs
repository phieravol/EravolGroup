using Eravol.UserWebApi.Data;
using Eravol.WebApi.Data.Models;

namespace Eravol.WebApi.Repositories.Experiences
{
    public class UserExperiencesRepository : IUserExperiencesRepository
    {
        private readonly EravolUserWebApiContext context;

        public UserExperiencesRepository(EravolUserWebApiContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Add User Experiences into database
        /// </summary>
        /// <param name="experience"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task CreateUserExperience(Experience experience)
        {
            try
            {
                context.Experiences.Add(experience);
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Delete User Experience
        /// </summary>
        /// <param name="currentExperience"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task DeleteUserExperience(Experience currentExperience)
        {
            try
            {
                context.Experiences.Remove(currentExperience);
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Get User Experience by current UserId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<Experience>? GetMyExperiences(Guid userId)
        {
            try
            {
                List<Experience>? experiences = context.Experiences.Where(x => x.UserId== userId).ToList();
                return experiences;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Get User Experience by Id
        /// </summary>
        /// <param name="experienceId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Experience?> GetUserExperienceById(int experienceId)
        {
            try
            {
                Experience? experience = await context.Experiences.FindAsync(experienceId);
                return experience;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Update current User Experience
        /// </summary>
        /// <param name="currentExperience"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task UpdateUserExperience(Experience currentExperience)
        {
            try
            {
                context.Experiences.Update(currentExperience);
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
