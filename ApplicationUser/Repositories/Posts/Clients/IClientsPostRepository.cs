using Eravol.WebApi.Data.Models;
using Eravol.WebApi.ViewModels.Base;
using Eravol.WebApi.ViewModels.Posts.Clients;

namespace Eravol.WebApi.Repositories.Posts.Clients
{
    public interface IClientsPostRepository
    {
        Task CreatePostAsync(Post post);
        Task DeletePostAsync(Post post);
        Task<Post?> GetPostById(int? postId);
        Task<List<Post>> GetPostSearchPaging(PagingRequestBase<Post> request, Guid UserId);
        Task UpdatePostAsync(Post post);
    }
}
