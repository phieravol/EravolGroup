using Eravlol.UserWebApi.Data.Models;
using Eravol.WebApi.Data.Models;
using Eravol.WebApi.Repositories.Posts.Public;
using Eravol.WebApi.ViewModels.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Security.Claims;

namespace Eravol.WebApi.Controllers.Posts.Public
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsPublicController : ControllerBase
    {
        private readonly IPostsPublicRepository postsPublicRepository;
		private readonly UserManager<AppUser> userManager;

		public PostsPublicController(
            IPostsPublicRepository postsPublicRepository,
            UserManager<AppUser> userManager
        )
        {
            this.postsPublicRepository = postsPublicRepository;
            this.userManager = userManager;
        }

        /// <summary>
        /// Get all post of public user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetPublicPosts([FromQuery] PagingRequestBase<Post> request)
        {
            //decode URL
            request.SearchTerm = WebUtility.UrlDecode(request.SearchTerm);
            request.PageSize = 6;

            //Get Posts paging by request
            List<Post> posts = await postsPublicRepository.GetPostSearchPaging(request);
            return Ok(posts);
        }

		[HttpGet]
		public async Task<IActionResult> GetFilterPagingPublicPosts([FromQuery] PagingRequestBase<Post> request)
		{
			return Ok();
		}


		/// <summary>
		/// Get Post detail for freelancer
		/// </summary>
		/// <param name="postId"></param>
		/// <returns></returns>
		[HttpGet("{postId}")]
		public async Task<IActionResult> GetPublicPostById(int? postId)
		{
            if (postId == null)
            {
                return BadRequest("Post Id is empty!");
            }

            Post? post = await postsPublicRepository.GetPublicPostById(postId);
			return Ok(post);
		}

	}
}
