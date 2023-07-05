using Eravol.WebApi.Data.Models;
using Eravol.WebApi.ViewModels.Base;
using Eravol.WebApi.ViewModels.PostStatuses;

namespace Eravol.WebApi.Repositories.PostStatuses.Clients
{
    public interface IPostStatusesRepository
    {
        Task CreatePostStatusAsync(UpdatePostStatusRequest postStatus);
        Task DeletePostStatusAsync(PostStatus postStatus);
        Task<PostStatus> GetPostStatusByIdAsync(int? postStatusId);
        Task<List<PostStatus>> GetPostStatusSearchPaging(PagingRequestBase<PostStatus> request);
        Task UpdatePostStatusAsync(PostStatus postStatus);
    }
}
