using Eravlol.UserWebApi.Data.Models;
using Eravol.UIClient.Repositories.Users.Profiles;
using Eravol.UserWebApi.Data.Models;
using Eravol.WebApi.Data.Models;
using Eravol.WebApi.ViewModels.UserSkills;
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
        [BindProperty(SupportsGet = true)] public List<Skill>? Skills { get; set; }
        [BindProperty(SupportsGet = true)] public List<UserSkillViewModel>? UserSkills { get; set; }
        [BindProperty(SupportsGet = true)] public List<Experience>? UserExperiences { get; set; }
        [BindProperty(SupportsGet = true)] public List<Portfolio>? Portfolios { get; set; }

		public async Task<IActionResult> OnGetAsync()
        {
			// get token from session
			token = HttpContext.Session.GetString("AuthToken");

			// check if user is logged or not
			if (token == null)
			{
				return RedirectToPage("/Forbidden");
            }

			//Get User information from backend api
			appUser = await profileService.GetUserInformation(token);

			//Get User Profile Images
			profileImages = await profileService.GetUserProfileImages(token);

			//Get User Avatar Images
			userAvatars = await profileService.GetUserAvatarImage(token);

			//Get all public skills
			Skills = await profileService.GetAllPublicSkills();

			//Get all User skills
			UserSkills = await profileService.GetMyUserSkills(token);

			//Get all Experience of current user
			UserExperiences = await profileService.GetMyExperiences(token);
			
			//Get all Portfolio of current user
			Portfolios = await profileService.GetMyPortfolios(token);
			return Page();
        }
    }
}

// response.value.