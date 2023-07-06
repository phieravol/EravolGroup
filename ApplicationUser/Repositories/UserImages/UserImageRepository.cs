using Eravol.UserWebApi.Data;
using Eravol.UserWebApi.Data.Models;
using System.Net.Http.Headers;

namespace Eravol.WebApi.Repositories.UserImages
{
    public class UserImageRepository : IUserImageRepository
    {
        #region Dependency Injection Services
        private readonly EravolUserWebApiContext context;
        #endregion

        #region Constructor
        public UserImageRepository(EravolUserWebApiContext context)
        {
            this.context = context;
        }
        #endregion

        /// <summary>
        /// Add UserImage object to database
        /// </summary>
        /// <param name="avatar">UserImage Object</param>
        /// <returns>No return</returns>
        /// <exception cref="Exception"></exception>
        public async Task AddUserAvatarToDb(UserImage avatar)
        {
            try
            {
                context.UserImages.Add(avatar);
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Delete specific user profile image by image name
        /// </summary>
        /// <param name="profileImage"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task DeleteImageById(UserImage userImage)
        {
            try
            {
                context.UserImages.Remove(userImage);
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Get UserImage by Id
        /// </summary>
        /// <param name="imageId">Id of UserImage object</param>
        /// <returns></returns>
        /// <exception cref="Exception">Specified UserImage object</exception>
        public UserImage? GetUserImageById(int? imageId)
        {
            try
            {
                UserImage? userImage = context.UserImages.Find(imageId);
                return userImage;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        
    }
}
