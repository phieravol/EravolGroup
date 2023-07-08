using Eravol.WebApi.Data.Models;

namespace Eravol.WebApi.Repositories.Experiences
{
    public interface IUserExperiencesRepository
    {
        Task CreateUserExperience(Experience experience);
        Task DeleteUserExperience(Experience currentExperience);
        List<Experience>? GetMyExperiences(Guid userId);
        Task<Experience?> GetUserExperienceById(int experienceId);
        Task UpdateUserExperience(Experience currentExperience);
    }
}
