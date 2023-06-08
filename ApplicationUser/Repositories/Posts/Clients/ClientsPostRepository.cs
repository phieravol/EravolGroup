using Eravol.UserWebApi.Data;
using Eravol.WebApi.Data.Models;
using Eravol.WebApi.ViewModels.Base;
using Eravol.WebApi.ViewModels.Posts.Clients;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;

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
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task DeletePostAsync(Post post)
        {
            try
            {
                context.Posts.Remove(post);
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Post?> GetPostById(int? postId)
        {
            Post? post = new Post();
            try
            {
                var query = from p in context.Posts
                            join c in context.Categories on p.CategoryId equals c.CategoryId into subCatejoined
                            from cateJoined in subCatejoined.DefaultIfEmpty()
                            join u in context.AppUsers on p.UserId equals u.Id
                            select new { p, cateJoined, u };

                post = await query.Select(x => new Post()
                {
                    PostId = x.p.PostId,
                    PostTitle = x.p.PostTitle,
                    SortDesc = x.p.SortDesc,
                    PostDetails = x.p.PostDetails,
                    UserId = x.p.UserId,
                    AppUser = x.u,
                    Budget = x.p.Budget,
                    Categories = x.cateJoined,
                    CategoryId = x.p.CategoryId,
                    ExpirationDate = x.p.ExpirationDate,
                    LevelRequired = x.p.LevelRequired,
                    PostedDate = x.p.PostedDate,
                    PostSkillRequired = x.p.PostSkillRequired,
                    PostStatus = x.p.PostStatus,
                    PostStatusId = x.p.PostStatusId,
                }).FirstOrDefaultAsync(x => x.PostId == postId);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return post;
        }

        public async Task<List<Post>> GetPostSearchPaging(PagingRequestBase<Post> request, Guid UserId)
        {
            List<Post> posts = new List<Post>();
            try
            {
                var query = from p in context.Posts
                            join c in context.Categories on p.CategoryId equals c.CategoryId into subCatejoined
                            from cateJoined in subCatejoined.DefaultIfEmpty()
                            join u in context.AppUsers on p.UserId equals u.Id
                            where p.UserId == UserId
                            select new {p, cateJoined, u};

                if (!string.IsNullOrEmpty(request.SearchTerm))
                {
                    query = query.Where(x => x.p.PostTitle.Contains(request.SearchTerm) || 
                            x.p.SortDesc.Contains(request.SearchTerm) ||
                            x.p.PostDetails.Contains(request.SearchTerm));
                }

                posts = query.Select(x => new Post()
                {
                    PostId = x.p.PostId,
                    PostTitle= x.p.PostTitle,
                    SortDesc= x.p.SortDesc,
                    PostDetails= x.p.PostDetails,
                    UserId = UserId,
                    AppUser= x.u,
                    Budget= x.p.Budget,
                    Categories= x.cateJoined,
                    CategoryId= x.p.CategoryId,
                    ExpirationDate= x.p.ExpirationDate,
                    LevelRequired= x.p.LevelRequired,
                    PostedDate= x.p.PostedDate,
                    PostSkillRequired= x.p.PostSkillRequired,
                    PostStatus= x.p.PostStatus,
                    PostStatusId = x.p.PostStatusId,
                }).ToList();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return posts;
        }

        public async Task UpdatePostAsync(Post post)
        {
            try
            {
                context.Entry<Post>(post).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
