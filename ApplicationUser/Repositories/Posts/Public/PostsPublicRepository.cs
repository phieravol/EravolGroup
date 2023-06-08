using Eravol.UserWebApi.Data;
using Eravol.WebApi.Data.Models;
using Eravol.WebApi.ViewModels.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Eravol.WebApi.Repositories.Posts.Public
{
    public class PostsPublicRepository : IPostsPublicRepository
    {
        private readonly EravolUserWebApiContext context;

        public PostsPublicRepository(EravolUserWebApiContext context)
        {
            this.context = context;
        }

        public async Task<List<Post>> GetPostSearchPaging(PagingRequestBase<Post> request)
        {
            List<Post> posts = new List<Post>();
            try
            {
                var query = from p in context.Posts
                            join c in context.Categories on p.CategoryId equals c.CategoryId into subCatejoined
                            from cateJoined in subCatejoined.DefaultIfEmpty()
                            join u in context.AppUsers on p.UserId equals u.Id
                            select new { p, cateJoined, u };

                if (!string.IsNullOrEmpty(request.SearchTerm))
                {
                    query = query.Where(x => x.p.PostTitle.Contains(request.SearchTerm) ||
                            x.p.SortDesc.Contains(request.SearchTerm) ||
                            x.p.PostDetails.Contains(request.SearchTerm));
                }

                posts = await query.Select(x => new Post()
                {
                    PostId = x.p.PostId,
                    PostTitle = x.p.PostTitle,
                    SortDesc = x.p.SortDesc,
                    PostDetails = x.p.PostDetails,
                    UserId = x.u.Id,
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
                }).ToListAsync();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return posts;
        }

		public async Task<Post?> GetPublicPostById(int? postId)
        {
            Post? post = new Post();
			try
			{
				var query = from p in context.Posts
							join c in context.Categories on p.CategoryId equals c.CategoryId into subCatejoined
							from cateJoined in subCatejoined.DefaultIfEmpty()
							join u in context.AppUsers on p.UserId equals u.Id
							join ps in context.PostStatuses on p.PostStatusId equals ps.PostStatusId into subStatus
							from statusJoined in subStatus.DefaultIfEmpty()
							where p.PostId == postId
							select new { p, cateJoined, u, statusJoined };

				
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
				}).FirstOrDefaultAsync();

			}
			catch (Exception e)
            {
				throw new Exception(e.Message);
			}
			return post;
			

		}
	}
}
