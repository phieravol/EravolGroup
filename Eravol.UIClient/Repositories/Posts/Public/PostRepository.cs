using Eravol.WebApi.Data.Models;
using Eravol.WebApi.ViewModels.PostSkillRequires;
using Eravol.WebApi.ViewModels.Services.Public;
using Newtonsoft.Json;

namespace Eravol.UIClient.Repositories.Posts.Public
{
	public class PostRepository : IPostRepository
	{
		const string BASE_URL = "https://localhost:7259";
		string POST_PATH_URL = $"api/PostsPublic/";
		string SKILLREQUIRE_PATH_URL = $"/api/PostSkillRequire/";

		private readonly IHttpClientFactory clientFactory;

		public PostRepository(IHttpClientFactory clientFactory)
		{
			this.clientFactory = clientFactory;
		}

		public async Task<Post?> GetPostDetailsByIdAsync(int? postId)
		{
			string url = POST_PATH_URL + postId.ToString();
			var client = clientFactory.CreateClient();

			client.BaseAddress = new Uri(BASE_URL);
			HttpResponseMessage response = await client.GetAsync(url);

			string dataResponse = await response.Content.ReadAsStringAsync();
			
			if (dataResponse =="Post not found!")
			{
				return null;
			}
			Post? post = JsonConvert.DeserializeObject<Post?>(dataResponse);
			return post;
		}

		public async Task<List<PostSkillRequireViewModel>> getPostSkillRequiresByPostId(int? postId)
		{
			string url = SKILLREQUIRE_PATH_URL + postId.ToString();
			var client = clientFactory.CreateClient();

			client.BaseAddress = new Uri(BASE_URL);
			HttpResponseMessage response = await client.GetAsync(url);

			string dataResponse = await response.Content.ReadAsStringAsync();

			if (dataResponse == "Post not found!")
			{
				return null;
			}
			List<PostSkillRequireViewModel>? skillRequires = JsonConvert.DeserializeObject<List<PostSkillRequireViewModel>?>(dataResponse);
			return skillRequires;
		}
	}
}
