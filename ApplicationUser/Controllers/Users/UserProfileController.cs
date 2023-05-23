using Eravlol.UserWebApi.Data.Models;
using Eravol.UserWebApi.Repository.User.Admin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Eravol.WebApi.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IManageProfileRepository profileRepository;
        private readonly UserManager<AppUser> userManager;

        public UserProfileController(IManageProfileRepository profileRepository, UserManager<AppUser> userManager)
        {
            this.profileRepository = profileRepository;
            this.userManager = userManager;
        }

        [HttpGet("{UserName}")]
        public async Task<ActionResult<AppUser>> GetProfile(string? UserName)
        {
            if (string.IsNullOrWhiteSpace(UserName))
            {
                return BadRequest("Username parameter is empty!");
            }
            AppUser? appUser = await profileRepository.GetUserByUsername(UserName);

            if (appUser == null)
            {
                return NotFound("User can't be found in the system!");
            }

            return appUser;
        }
    }
}
