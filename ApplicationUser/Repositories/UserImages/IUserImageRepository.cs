using Eravol.UserWebApi.Data.Models;

namespace Eravol.WebApi.Repositories.UserImages
{
    public interface IUserImageRepository
    {
        Task AddUserAvatarToDb(UserImage avatar);
        Task DeleteImageById(UserImage userImage);
        UserImage? GetUserImageById(int? imageId);
    }
}
