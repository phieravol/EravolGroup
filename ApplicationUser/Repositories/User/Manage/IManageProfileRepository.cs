using Eravlol.UserWebApi.Data.Models;
using Eravol.UserWebApi.Data.Models;

namespace Eravol.UserWebApi.Repository.User.Admin
{
	public interface IManageProfileRepository
	{
        Task<List<UserImage>> AddProfileImagesToDb(Guid UserId, List<IFormFile> profileImages);
		Task<UserImage?> GetCurrentAvatarById(Guid userId);
		Task<List<UserImage>> GetUseAvatarById(Guid userId);
        Task<AppUser?> GetUserByUsername(string? userName);
        Task<List<UserImage>> GetUserImagesById(Guid userId);
    }
}
