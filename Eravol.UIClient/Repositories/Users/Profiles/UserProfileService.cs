using Eravlol.UserWebApi.Data.Models;
using Eravol.UserWebApi.Data.Models;
using Eravol.WebApi.Data.Models;
using Eravol.WebApi.ViewModels.Services.Freelancers;
using Eravol.WebApi.ViewModels.UserSkills;
using Newtonsoft.Json;
using NuGet.Common;
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
		private string SKILL_PATH_URL = $"/api/PublicSkills/PublicSkills";
		private string MYSKILLS_PATH_URL = $"/api/PublicUserSkills/MySkills";
		private string EXPERIENCE_PATH_URL = $"/api/Experiences";
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

        public async Task<List<Skill>?> GetAllPublicSkills()
        {
            //Create new client to send request to api
            var client = clientFactory.CreateClient();
            client.BaseAddress = new Uri(BASE_URL);
            HttpResponseMessage response = await client.GetAsync(SKILL_PATH_URL);

            string dataResponse = await response.Content.ReadAsStringAsync();
            List<Skill>? skills = JsonConvert.DeserializeObject<List<Skill>?>(dataResponse);
            return skills;
        }

        public async Task<List<UserSkillViewModel>?> GetMyUserSkills(string token)
        {
            //Create new client to send request to api
            var client = clientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            client.BaseAddress = new Uri(BASE_URL);
            HttpResponseMessage response = await client.GetAsync(MYSKILLS_PATH_URL);

            string dataResponse = await response.Content.ReadAsStringAsync();
            List<UserSkillViewModel>? profileImages = JsonConvert.DeserializeObject<List<UserSkillViewModel>?>(dataResponse);
            return profileImages;
        }

        public async Task<List<Experience>?> GetMyExperiences(string token)
        {
            //Create new client to send request to api
            var client = clientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            client.BaseAddress = new Uri(BASE_URL);
            HttpResponseMessage response = await client.GetAsync(EXPERIENCE_PATH_URL);

            string dataResponse = await response.Content.ReadAsStringAsync();
            List<Experience>? profileImages = JsonConvert.DeserializeObject<List<Experience>?>(dataResponse);
            return profileImages;
        }
    }
}
