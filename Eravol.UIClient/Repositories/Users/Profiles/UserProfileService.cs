using Eravlol.UserWebApi.Data.Models;
using Eravol.UserWebApi.Data.Models;
using Eravol.WebApi.ViewModels.Services.Freelancers;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Policy;

namespace Eravol.UIClient.Repositories.Users.Profiles
{
	public class UserProfileService : IUserProfileService
	{
		#region Constants
		private const string BASE_URL = "https://localhost:7259";
		private string PROFILE_PATH_URL = $"/api/UserProfile";
		private string USERIMAGE_PATH_URL = $"/api/UserImages";
		private string AVATAR_PATH_URL = $"/api/UserImages/UserAvatar";
		#endregion

		#region Dependency Injection Services
		private readonly IHttpClientFactory clientFactory;
		#endregion

		#region Constructor
		public UserProfileService(IHttpClientFactory clientFactory)
		{
			this.clientFactory = clientFactory;
		}
		#endregion

		public async Task<AppUser?> GetUserInformation(string token)
		{
			//Create new client to send request to api
			var client = clientFactory.CreateClient();
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			client.BaseAddress = new Uri(BASE_URL);
			HttpResponseMessage response = await client.GetAsync(PROFILE_PATH_URL);

			string dataResponse = await response.Content.ReadAsStringAsync();
			AppUser? appUser = JsonConvert.DeserializeObject<AppUser>(dataResponse);
			return appUser;
		}

        public async Task<List<UserImage>?> GetUserProfileImages(string token)
        {
            //Create new client to send request to api
            var client = clientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            client.BaseAddress = new Uri(BASE_URL);
            HttpResponseMessage response = await client.GetAsync(USERIMAGE_PATH_URL);

            string dataResponse = await response.Content.ReadAsStringAsync();
            List<UserImage>? profileImages = JsonConvert.DeserializeObject<List<UserImage>?>(dataResponse);
            return profileImages;
        }

        public async Task<List<UserImage>?> GetUserAvatarImage(string token)
        {
            //Create new client to send request to api
            var client = clientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            client.BaseAddress = new Uri(BASE_URL);
            HttpResponseMessage response = await client.GetAsync(AVATAR_PATH_URL);

            string dataResponse = await response.Content.ReadAsStringAsync();
            List<UserImage>? profileImages = JsonConvert.DeserializeObject<List<UserImage>?>(dataResponse);
            return profileImages;
        }
    }
}
