using Eravlol.UserWebApi.Data.Models;
using Eravol.UserWebApi.Data.Models;
using Eravol.UserWebApi.Repository.User.Admin;
using Eravol.WebApi.ViewModels.Users.Profiles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using System.Diagnostics;
using System.Security.Claims;

namespace Eravol.WebApi.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        #region Dependency Injection services
        private readonly IManageProfileRepository profileRepository;
        private readonly UserManager<AppUser> userManager;
        #endregion

        #region Constructor
        public UserProfileController(IManageProfileRepository profileRepository, UserManager<AppUser> userManager)
        {
            this.profileRepository = profileRepository;
            this.userManager = userManager;
        }
        #endregion

        /// <summary>
        /// Get user profile from database
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<AppUser>> GetProfile()
        {
            //Get UserId by claims in token
			string? UserIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            /* Check UserId is null or not null */
            if (string.IsNullOrWhiteSpace(UserIdStr))
            {
                return BadRequest("UserId can not be null");
            }

            AppUser? appUser = await profileRepository.GetUserByUsername(UserIdStr);

            if (appUser == null)
            {
                return NotFound("User can't be found in the system!");
            }

            return appUser;
        }

        /// <summary>
        /// Create User profile images
        /// </summary>
        /// <param name="profileImages"></param>
        /// <returns></returns>
        [HttpPost("ProfileImages")]
        [Authorize]
        public async Task<IActionResult> AddProfileImages(List<IFormFile>? profileImages)
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

            //Return error if user not choose images
            if (profileImages == null) 
            { 
                return BadRequest("Please choose least 1 profile image!"); 
            }

            //Add Images to database
            List<UserImage> responseImages = await profileRepository.AddProfileImagesToDb(UserId, profileImages);

            return Ok(responseImages);
        }

        [HttpPut("ProfileInformation")]
        [Authorize]
        public async Task<IActionResult> UpdateProfileInfor(UserProfileViewModel ProfileInfor)
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

            //Get AppUser by UserId
            AppUser? appUser = await profileRepository.GetAppUserById(UserId);

            if (appUser == null)
            {
                return NotFound("User not found");
            }

            appUser.FirstName= ProfileInfor.FirstName;
            appUser.LastName= ProfileInfor.LastName;
            appUser.PhoneNumber= ProfileInfor.PhoneNumber;
            appUser.Address= ProfileInfor.Address;
			appUser.Tagline = ProfileInfor.Tagline;
            appUser.Description = ProfileInfor.Description;
            appUser.Country = ProfileInfor.Country;
            appUser.Birthday = ProfileInfor?.Birthday;

            //Update appUser
            await profileRepository.UpdateAppUser(appUser);

			return Ok(appUser);
        }

	}
}
