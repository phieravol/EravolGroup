using Eravol.UserWebApi.Data;
using Eravol.WebApi.Data.Models;

namespace Eravol.WebApi.Repositories.Posts.Clients
{
    public class ClientsPostRepository : IClientsPostRepository
    {
        private readonly EravolUserWebApiContext context;

        public ClientsPostRepository(EravolUserWebApiContext context)
        {
            this.context = context;
        }

        public async Task CreatePostAsync(Post post)
        {
            try
            {
                context.Posts.Add(post);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
