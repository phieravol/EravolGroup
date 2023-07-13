using Eravlol.UserWebApi.Data.Models;
using Eravol.UserWebApi.Data.Models;
using Eravol.WebApi.Data.Models;
using Eravol.WebApi.ViewModels.UserSkills;

namespace Eravol.UIClient.Repositories.Users.Profiles
{
	public interface IUserProfileService
	{
		Task<AppUser?> GetUserInformation(string token);
        Task<List<UserImage>?> GetUserProfileImages(string token);
        Task<List<UserImage>?> GetUserAvatarImage(string token);
        Task<List<Skill>?> GetAllPublicSkills();
        Task<List<UserSkillViewModel>?> GetMyUserSkills(string token);
        Task<List<Experience>?> GetMyExperiences(string token);
        Task<List<Portfolio>?> GetMyPortfolios(string token);
        Task<List<Certificate>?> GetMyCertificates(string token);
    }
}
