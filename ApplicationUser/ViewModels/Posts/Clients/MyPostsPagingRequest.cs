using Eravol.WebApi.Data.Models;
using Eravol.WebApi.ViewModels.Base;

namespace Eravol.WebApi.ViewModels.Posts.Clients
{
    public class MyPostsPagingRequest: PagingRequestBase<Post>
    {
        public string UserName { get; set; }
    }
}
