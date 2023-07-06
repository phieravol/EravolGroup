using Eravol.UIClient.Repositories.Users.Profiles;
using Eravol.UIClient.ViewModels.Users.Public;
using Eravol.UserWebApi.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using NuGet.Common;
using System.Data;
using System.Diagnostics.Metrics;
using System.Net.Http.Headers;
using System.Net;

namespace Eravol.UIClient.Pages.Components.HeaderComponent
{
	public class HeaderComponent : ViewComponent
	{
		#region Constants
		const string ADMIN = "Admin";
		const string FREELANCER = "Freelancer";
		const string CLIENT = "Client";
		private const string DEFAULT_AVATAR = "img-07.jpg";
		private const string BASE_URL = "https://localhost:7259";
		private const string AVATAR_URL = "/api/UserImages/CurrentAvatar";

		#endregion

		private readonly IHttpClientFactory clientFactory;
		private readonly IUserProfileService userProfileService;

        public HeaderComponent(
			IHttpClientFactory clientFactory,
            IUserProfileService userProfileService
		)
        {
            this.clientFactory = clientFactory;
			this.userProfileService = userProfileService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
		{
			// get token from session
			string? token = HttpContext.Session.GetString("AuthToken");

			// check if user is logged or not
			string? userAvatar = null;
			if (token != null)
			{
				userAvatar = await getUserAvatar(token);
			}
			string? role = HttpContext.Session.GetString("Roles");
			
			UserInforViewModes? userInfo = new UserInforViewModes()
			{
				UserId = HttpContext.Session.GetString("UserId"),
				Email = HttpContext.Session.GetString("Email"),
				FullName= HttpContext.Session.GetString("Fullname"),
				PhoneNumber= HttpContext.Session.GetString("PhoneNumber"),
				Role= role,
				UserName = HttpContext.Session.GetString("Username"),
				UserAvatar = userAvatar
			};
			return View("HeaderComponent", userInfo);
		}

		private async Task<string?> getUserAvatar(string? token)
		{
			string currentAvatarName = null;
			//Create new client to send request to api
			var client = clientFactory.CreateClient();
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			client.BaseAddress = new Uri(BASE_URL);
			HttpResponseMessage response = await client.GetAsync(AVATAR_URL);

			if (response.StatusCode == HttpStatusCode.NoContent)
			{
				return DEFAULT_AVATAR;
			}
			string dataResponse = await response.Content.ReadAsStringAsync();
			
			UserImage? currentAvatar = JsonConvert.DeserializeObject<UserImage?>(dataResponse);
			if (currentAvatar == null)
			{
				return DEFAULT_AVATAR;
			}
			currentAvatarName = currentAvatar.UserImageName==null? DEFAULT_AVATAR: currentAvatar.UserImageName;
			return currentAvatarName;
		}
	}
}
