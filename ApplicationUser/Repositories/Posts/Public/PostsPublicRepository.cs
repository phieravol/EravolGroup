using Eravol.UserWebApi.Data;
using Eravol.WebApi.Data.Models;
using Eravol.WebApi.ViewModels.Base;
using Eravol.WebApi.ViewModels.Posts.Public;
using Eravol.WebApi.ViewModels.PostSkillRequires;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Eravol.WebApi.Repositories.Posts.Public
{
    public class PostsPublicRepository : IPostsPublicRepository
    {
		#region Constants
		private const decimal FILTER_LIMIT = 150;
		#endregion

		#region DBContext Dependency Service
		private readonly EravolUserWebApiContext context;
		#endregion

		#region Constructor
		public PostsPublicRepository(EravolUserWebApiContext context)
        {
            this.context = context;
        }
		#endregion

		/// <summary>
		/// Get public Post filter paging
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<List<PostPublicViewModel>> GetPostFilterPaging(PostPublicFilterPaging request)
		{
			List<PostPublicFilterPaging> posts= new List<PostPublicFilterPaging>();

			try
			{
				var query = from p in context.Posts
							join u in context.AppUsers on p.UserId equals u.Id
							join c in context.Categories on p.CategoryId equals c.CategoryId into pcJoined
							from c in pcJoined.DefaultIfEmpty()
							join ps in context.PostStatuses on p.PostStatusId equals ps.PostStatusId into psJoined
							from ps in psJoined.DefaultIfEmpty()
							select new
							{
								p,
								u,
								c,
								ps
							};
				//Filter Post by Search term if user enter keyword
				if (!string.IsNullOrEmpty(request.SearchTerm))
				{
					query = query.Where(x => x.p.PostTitle.Contains(request.SearchTerm) || x.u.UserName.Contains(request.SearchTerm) || x.p.SortDesc.Contains(request.SearchTerm));
				}

				//Filter Post by CategoryIds
				if (request.categoryFilters != null)
				{
					query = query.Where(x => request.categoryFilters.Contains((int)x.c.CategoryId));
				}

				/* filter service by price value when maxFilter reach out limit */
				if (request.MaxPrice == FILTER_LIMIT)
				{
					query = query.Where(x => x.p.Budget >= FILTER_LIMIT);
				}
				else
				{
					//filter service by price value in range filter limit
					query = query.Where(x => x.p.Budget >= request.MinPrice && x.p.Budget <= request.MaxPrice);
				}

				List<PostPublicViewModel> postPublics = await query.Select(x => new PostPublicViewModel()
				{
					PostId = x.p.PostId,
					PostTitle = x.p.PostTitle,
					SortDesc = x.p.SortDesc,
					PostDetails = x.p.PostDetails,
					Budget = x.p.Budget,
					CategoryId = x.p.CategoryId,
					CategoryName = x.c.CategoryName,
					ExpirationDate = x.p.ExpirationDate,
					LastUpdatedDate = x.p.LastUpdatedDate,
					LevelRequired = x.p.LevelRequired,
					PostedDate = x.p.PostedDate,
					PostStatusId = x.p.PostStatusId,
					UserId = x.p.UserId,
					FullName = $"{x.u.FirstName} {x.u.LastName}",
					Country = x.u.Country,
					Username = x.u.UserName
				}).ToListAsync();

				if (postPublics != null)
				{
					foreach (var post in postPublics)
					{
						var skillRequireQuery = from require in context.PostSkilRequires
												join skill in context.Skills on require.SkillId equals skill.Id
												where require.PostId == post.PostId
												select new
												{
													require,
													skill
												};
						post.SkillRequire = skillRequireQuery.Select(sr => new PostSkillRequireViewModel()
						{
							PostId = post.PostId,
							SkillId = sr.require.SkillId,
							SkillName = sr.skill.SkillName
						}).ToList();
					}

				}

				request.PageSize = 3;
				//Set totoal pages for paging
				request.TotalPages = (int)Math.Ceiling(postPublics.Count() / (double)request.PageSize);

				//Get Services in each pages
				postPublics = postPublics.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize).ToList();
				//Set Items in each pages
				request.Items = postPublics;
				return postPublics;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Get Post search paging
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
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
