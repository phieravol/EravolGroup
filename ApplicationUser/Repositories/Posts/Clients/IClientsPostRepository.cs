using Eravol.WebApi.Data.Models;

namespace Eravol.WebApi.Repositories.Posts.Clients
{
    public interface IClientsPostRepository
    {
        Task CreatePostAsync(Post post);
    }
}
