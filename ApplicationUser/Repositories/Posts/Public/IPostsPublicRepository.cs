using Eravol.WebApi.Data.Models;
using Eravol.WebApi.ViewModels.Base;
using Eravol.WebApi.ViewModels.Posts.Public;

namespace Eravol.WebApi.Repositories.Posts.Public
{
    public interface IPostsPublicRepository
    {
		Task<List<PostPublicViewModel>> GetPostFilterPaging(PostPublicFilterPaging request);
		Task<List<Post>> GetPostSearchPaging(PagingRequestBase<Post> request);
		Task<Post?> GetPublicPostById(int? postId);
	}
}
