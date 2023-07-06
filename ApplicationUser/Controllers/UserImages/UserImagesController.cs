using Eravol.UserWebApi.Data.Models;
using Eravol.UserWebApi.Repository.User.Admin;
using Eravol.WebApi.Repositories.Images;
using Eravol.WebApi.Repositories.UserImages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace Eravol.WebApi.Controllers.UserImages
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserImagesController : ControllerBase
    {
        #region
        private const string USER_CONTENT_FOLDER_NAME = "user-content";
        #endregion

        #region Dependency Injection Services
        private readonly IManageProfileRepository profileRepository;
        private readonly IUserImageRepository userImageRepository;
        private readonly IFileStorageService fileStorageService;
        #endregion

        #region Constructor
        public UserImagesController(
            IManageProfileRepository profileRepository,
            IUserImageRepository userImageRepository,
            IFileStorageService fileStorageService
        )
        {
            this.profileRepository = profileRepository;
            this.userImageRepository = userImageRepository;
            this.fileStorageService = fileStorageService;
        }
        #endregion

        /// <summary>
        /// Get List User avatar
        /// </summary>
        /// <returns></returns>
        [HttpGet("UserAvatar")]
        [Authorize]
        public async Task<IActionResult> GetUserAvatarImage()
        {
            //Get UserId by claims in token
            string? UserIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            /* Check UserId is null or not null */
            if (string.IsNullOrWhiteSpace(UserIdStr))
            {
                return BadRequest("UserId can not be null");
            }

            //Convert ID from string to GUID
            Guid UserId = Guid.Parse(UserIdStr);

            //get user avatar from database
            List<UserImage> Images = await profileRepository.GetUseAvatarById(UserId);
            return Ok(Images);
        }

		[HttpGet("CurrentAvatar")]
		[Authorize]
		public async Task<IActionResult> GetCurrentUserAvatar()
		{
			//Get UserId by claims in token
			string? UserIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			/* Check UserId is null or not null */
			if (string.IsNullOrWhiteSpace(UserIdStr))
			{
				return BadRequest("UserId can not be null");
			}

			//Convert ID from string to GUID
			Guid UserId = Guid.Parse(UserIdStr);

			//get user avatar from database
			UserImage? currentAvatar = await profileRepository.GetCurrentAvatarById(UserId);
			return Ok(currentAvatar);
		}

		/// <summary>
		/// Get User profile images
		/// </summary>
		/// <returns></returns>
		[HttpGet]
        [Authorize]
        public async Task<IActionResult> GetProfileImages()
        {
            //Get UserId by claims in token
            string? UserIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            /* Check UserId is null or not null */
            if (string.IsNullOrWhiteSpace(UserIdStr))
            {
                return BadRequest("UserId can not be null");
            }

            //Convert ID from string to GUID
            Guid UserId = Guid.Parse(UserIdStr);
            List<UserImage> Images = await profileRepository.GetUserImagesById(UserId);
            return Ok(Images);
        }

        /// <summary>
        /// Create UserAvatar into database
        /// </summary>
        /// <param name="userAvatar"></param>
        /// <returns></returns>
        [HttpPost("UserAvatar")]
        [Authorize]
        public async Task<IActionResult> CreateUserAvatar(IFormFile? userAvatar)
        {
            //Get UserId by claims in token
            string? UserIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            /* return error message if UserId is empty */
            if (string.IsNullOrWhiteSpace(UserIdStr))
            {
                return BadRequest("UserId can not be null");
            }

            //Convert ID from string to GUID
            Guid UserId = Guid.Parse(UserIdStr);

            //Create a new UserImage
            UserImage avatar = new UserImage()
            {
                UserId = UserId,
                isUserAvatar = true,
                isThumbnail = false,
                DateCreated = DateTime.Now,
                UserImageSize = userAvatar.Length
            };

            //Add image to folder
            avatar.UserImageName = await SaveFile(userAvatar);
            avatar.UserImagePath = "/" + USER_CONTENT_FOLDER_NAME + "/" + avatar.UserImageName;
            //Add image into database
            await userImageRepository.AddUserAvatarToDb(avatar);
            return Ok(avatar);
        }


        /// <summary>
        /// Delete User Image by UserImageId
        /// </summary>
        /// <param name="imageId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteProfileImageByName(int? imageId)
        {
            // display message when imageId null
            if (imageId == null)
            {
                return BadRequest("You didn't choose profile image to delete.");
            }

            // Get UserImage by Id
            UserImage? userImage = userImageRepository.GetUserImageById(imageId);

            //display message when userImage not found
            if (userImage == null)
            {
                return NotFound("User Image not found!");
            }

            //remove userImage in database
            await userImageRepository.DeleteImageById(userImage);

            //remove userImage picture in folder
            string? imageName = userImage.UserImageName;
            if (!string.IsNullOrWhiteSpace(imageName))
            {
                await fileStorageService.DeleteFileAsync(imageName);
            }
            return NoContent();
        }

        /// <summary>
        /// Save file to foleder
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await fileStorageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
    }
}
