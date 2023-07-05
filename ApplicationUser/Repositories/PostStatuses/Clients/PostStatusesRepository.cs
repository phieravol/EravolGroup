using Eravol.UserWebApi.Data;
using Eravol.WebApi.Data.Models;
using Eravol.WebApi.ViewModels.Base;
using Eravol.WebApi.ViewModels.PostStatuses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Eravol.WebApi.Repositories.PostStatuses.Clients
{
    public class PostStatusesRepository : IPostStatusesRepository
    {
        private readonly EravolUserWebApiContext context;

        public PostStatusesRepository(EravolUserWebApiContext context)
        {
            this.context = context;
        }

        public async Task CreatePostStatusAsync(UpdatePostStatusRequest request)
        {
            try
            {
                PostStatus postStatus = new PostStatus()
                {
                    PostStatusName = request.PostStatusName,
                    PostStatusDesc = request.PostStatusDesc
                };
                context.PostStatuses.Add(postStatus);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeletePostStatusAsync(PostStatus postStatus)
        {
            try
            {
                context.PostStatuses.Remove(postStatus);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PostStatus> GetPostStatusByIdAsync(int? postStatusId)
        {
            try
            {
                PostStatus? postStatus = await context.PostStatuses.FindAsync(postStatusId);
                return postStatus;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<PostStatus>> GetPostStatusSearchPaging(PagingRequestBase<PostStatus> request)
        {
            try
            {
                IQueryable<PostStatus> query = context.PostStatuses;

                if (!string.IsNullOrEmpty(request.SearchTerm))
                {
                    query = query.Where(x => x.PostStatusName.Contains(request.SearchTerm) || x.PostStatusDesc.Contains(request.SearchTerm));
                }
                request.Items = await query.ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return request.Items;
        }

        public async Task UpdatePostStatusAsync(PostStatus postStatus)
        {
            try
            {
                context.Entry<PostStatus>(postStatus).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
