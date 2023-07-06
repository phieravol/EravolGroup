using Eravlol.UserWebApi.Data.Models;
using Eravol.UserWebApi.Data.Models;

namespace Eravol.UIClient.Repositories.Users.Profiles
{
	public interface IUserProfileService
	{
		Task<AppUser?> GetUserInformation(string token);
        Task<List<UserImage>?> GetUserProfileImages(string token);
        Task<List<UserImage>?> GetUserAvatarImage(string token);
    }
}
