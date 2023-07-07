using Eravlol.UserWebApi.Data.Models;
using Eravol.UserWebApi.Data;
using Eravol.UserWebApi.Data.Models;
using Eravol.WebApi.Data.Models;
using Eravol.WebApi.Repositories.Images;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

namespace Eravol.UserWebApi.Repository.User.Admin
{
	public class ManageProfileRepository : IManageProfileRepository
	{
        #region
        private const string USER_CONTENT_FOLDER_NAME = "user-content";
        #endregion

        #region Dependency Injection services
        private readonly EravolUserWebApiContext context;
        private readonly UserManager<AppUser> userManager;
        private readonly IFileStorageService storageService;
        #endregion

        #region Constructor
        public ManageProfileRepository(
            EravolUserWebApiContext context, 
            UserManager<AppUser> userManager,
            IFileStorageService storageService
        )
		{
			this.context = context;
			this.userManager = userManager;
            this.storageService = storageService;
		}
        #endregion


        public async Task<List<UserImage>> AddProfileImagesToDb(Guid UserId, List<IFormFile> profileImages)
        {
            if (profileImages == null)
            {
                return null;
            }

            List<UserImage> userImagesList = new List<UserImage>();

            foreach (var image in profileImages)
            {
                UserImage userImage = new UserImage()
                {
                    UserId = UserId,
                    UserImageSize = image.Length,
                    isThumbnail = false,
                    isUserAvatar = false,
                    DateCreated = DateTime.Now,
                };

                userImage.UserImageName = await SaveFile(image);
                userImage.UserImagePath = "/" + USER_CONTENT_FOLDER_NAME + "/" + userImage.UserImageName;

                userImagesList.Add(userImage);
            }

            try
            {
                context.UserImages.AddRange(userImagesList);
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return userImagesList;
        }

        /// <summary>
        /// Save file in folder
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }


        /// <summary>
        /// Get AppUser by username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<AppUser?> GetUserByUsername(string? userId)
		{
			return await context.AppUsers.FirstOrDefaultAsync(x => x.Id.ToString().Equals(userId));
		}

        /// <summary>
        /// Get user images that not avatar either banner
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<UserImage>> GetUserImagesById(Guid userId)
        {
            List<UserImage> userImages = new List<UserImage>();
            try
            {
                userImages = await context.UserImages
                    .Where(x => x.UserId == userId && !x.isUserAvatar && !x.isThumbnail)
                    .ToListAsync();
                return userImages;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Get User Avatar in database
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<UserImage>> GetUseAvatarById(Guid userId)
        {
            List<UserImage> userImages = new List<UserImage>();
            try
            {
                userImages = await context.UserImages
                    .Where(x => x.UserId == userId && x.isUserAvatar && !x.isThumbnail)
                    .ToListAsync();
                return userImages;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Get Current User avatar
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
		public async Task<UserImage?> GetCurrentAvatarById(Guid userId)
		{
            UserImage? currentAvatar = new UserImage();
			try
			{
				currentAvatar = await context.UserImages
					.Where(x => x.UserId == userId)
                    .OrderBy(x => x.ImgageId)
					.LastOrDefaultAsync(x => x.isUserAvatar);
				return currentAvatar;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

        /// <summary>
        /// Get AppUser by user Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
		public async Task<AppUser?> GetAppUserById(Guid userId)
		{
			AppUser? appUser = new AppUser();
			try
			{
				appUser = await context.AppUsers.FindAsync(userId);
                return appUser;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

        /// <summary>
        /// Update appUser
        /// </summary>
        /// <param name="appUser"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
		public async Task UpdateAppUser(AppUser appUser)
		{
			try
			{
                context.AppUsers.Update(appUser);
                await context.SaveChangesAsync();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
