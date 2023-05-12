using Eravlol.UserWebApi.Data.Models;
using Eravol.UserWebApi.Repository.User.Admin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eravol.UserWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IManageProfileRepository profileRepository;

		public UserProfileController(IManageProfileRepository profileRepository)
		{
			this.profileRepository = profileRepository;
		}

		[HttpGet("{UserName}")]
		public async Task<ActionResult<AppUser>> GetProfile(string? UserName)
		{
			if (string.IsNullOrWhiteSpace(UserName))
			{
				return BadRequest("Username parameter is empty!");
			}
			AppUser? appUser = await profileRepository.GetUserByUsername(UserName);

			//if (appUser == null)
			//{
			//	return NotFound("User is notfound!");
			//}
			return appUser;
		}
	}
}
