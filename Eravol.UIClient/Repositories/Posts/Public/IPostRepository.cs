using Eravol.WebApi.Data.Models;
using Eravol.WebApi.ViewModels.PostSkillRequires;

namespace Eravol.UIClient.Repositories.Posts.Public
{
	public interface IPostRepository
	{
		Task<Post?> GetPostDetailsByIdAsync(int? postId);
		Task<List<PostSkillRequireViewModel>> getPostSkillRequiresByPostId(int? postId);
	}
}
