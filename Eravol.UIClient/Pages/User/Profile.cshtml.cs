using Eravlol.UserWebApi.Data.Models;
using Eravol.UIClient.Repositories.Users.Profiles;
using Eravol.UserWebApi.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eravol.UIClient.Pages.User
{
    public class ProfileModel : PageModel
    {
        #region DI Services
        private readonly IUserProfileService profileService;
		#endregion

		#region Constructor
		public ProfileModel(IUserProfileService profileService)
		{
			this.profileService = profileService;
		}
		#endregion

		[BindProperty(SupportsGet = true)] public string? token { get; set; }
        [BindProperty(SupportsGet = true)] public AppUser? appUser { get; set; }
        [BindProperty(SupportsGet = true)] public List<UserImage>? profileImages { get; set; }
        [BindProperty(SupportsGet = true)] public List<UserImage>? userAvatars { get; set; }

		public async Task<IActionResult> OnGetAsync()
        {
			// get token from session
			token = HttpContext.Session.GetString("AuthToken");

			// check if user is logged or not
			if (token == null)
			{
				return RedirectToPage("/Forbidden");
            }

			// Get User information from backend api
			appUser = await profileService.GetUserInformation(token);

			//Get User Profile Images
			profileImages = await profileService.GetUserProfileImages(token);

			//Get User Avatar Images
			userAvatars = await profileService.GetUserAvatarImage(token);
			
			return Page();
        }
    }
}
