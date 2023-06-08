using Eravol.WebApi.Data.Models;
using Eravol.WebApi.ViewModels.Base;

namespace Eravol.WebApi.Repositories.Posts.Public
{
    public interface IPostsPublicRepository
    {
        Task<List<Post>> GetPostSearchPaging(PagingRequestBase<Post> request);
		Task<Post?> GetPublicPostById(int? postId);
	}
}
