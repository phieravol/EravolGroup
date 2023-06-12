using Eravlol.UserWebApi.Data.Models;
using Eravol.WebApi.Data.Models;
using Eravol.WebApi.Repositories.Posts.Clients;
using Eravol.WebApi.Repositories.PostSkills;
using Eravol.WebApi.ViewModels.Base;
using Eravol.WebApi.ViewModels.Categories;
using Eravol.WebApi.ViewModels.Posts.Clients;
using Eravol.WebApi.ViewModels.PostSkillRequires;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Net;
using System.Security.Claims;

namespace Eravol.WebApi.Controllers.Posts.Clients
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        #region DI Services
        private readonly IClientsPostRepository postsRepository;
        private readonly IPostSkillsRepository skillRequireRepository;
        private readonly UserManager<AppUser> userManager;
        #endregion

        #region Constructor
        public PostsController(
            IClientsPostRepository postsRepository,
            UserManager<AppUser> userManager,
            IPostSkillsRepository skillRequireRepository
        ) {
            this.postsRepository = postsRepository;
            this.userManager = userManager;
            this.skillRequireRepository = skillRequireRepository;
        }
        #endregion

        /// <summary>
        /// Get Post with paging in dashboard of clients
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetMyPosts([FromQuery] PagingRequestBase<Post> request)
        {
            //decode URL
            request.SearchTerm = WebUtility.UrlDecode(request.SearchTerm);

            //Get AppUser Id by claim
            string UserIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //If User not login then return message
            if (string.IsNullOrEmpty(UserIdStr))
            {
                return BadRequest("User Can't found in the session");
            }

            //Convert ID from string to GUID
            Guid UserId = Guid.Parse(UserIdStr);

            //Get Posts paging by request
            List<Post> posts = await postsRepository.GetServiceSearchPaging(request, UserId);

            //Set total Pages for paging
            request.TotalPages = (int)Math.Ceiling(posts.Count() / (double)request.PageSize);

            //Split Posts into list in each page
            posts = posts.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize).ToList();

            //set items for Paging ViewModel
            request.Items = posts;


            return Ok(request);
        }


        /// <summary>
        /// Create Post by CreatePostRequest ViewModel send by ajax
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreatePost(CreatePostRequest request)
        {
            
            string? UserIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (UserIdStr == null)
            {
                return BadRequest("Username can not null");
            }
            Guid UserId = Guid.Parse(UserIdStr);
            Post post = new Post()
            {
                PostTitle = request.PostTitle,
                SortDesc = request.SortDesc,
                PostDetails = request.PostDetails,
                PostStatusId = request.PostStatusId,
                PostedDate = DateTime.Now,
                CategoryId = request.CategoryId,
                Budget = request.Budget,
                ExpirationDate = request.ExpirationDate,
                LevelRequired = request.LevelRequired,
                UserId = UserId
            };

            await postsRepository.CreatePostAsync(post);
            int postId = post.PostId;
            return Created("./Index", postId);
        }

        [HttpGet("{PostId}")]
        public async Task<IActionResult> GetPostById(int? PostId)
        {
            if (PostId == null) return BadRequest("PostId can not be empty");

            Post? post = await postsRepository.GetPostById(PostId);
            if (post == null) return BadRequest("Post Not Found");

            return Ok(post);
        }

        [HttpPut("{PostId}")]
        public async Task<IActionResult> UpdatePostById(int? PostId, UpdatePostRequest request)
        {
            if (PostId == null) return BadRequest("PostId can not be empty");
            
            string? UserIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(UserIdStr))
            {
                return NotFound("User is not login, please login");
            }
            Guid UserId = Guid.Parse(UserIdStr);

            Post? post = await postsRepository.GetPostById(PostId);
            if (post == null)
            {
                return NotFound("Post not found");
            }
            post.PostId = request.PostId;
            post.PostStatusId= request.PostStatusId;
            post.PostTitle= request.PostTitle;
            post.SortDesc= request.SortDesc;
            post.PostDetails= request.PostDetails;
            post.ExpirationDate= request.ExpirationDate;
            post.LastUpdatedDate = DateTime.Now;
            post.Budget= request.Budget;
            post.CategoryId= request.CategoryId;
            
            await postsRepository.UpdatePostAsync(post);
            return NoContent();
        }

        [HttpDelete("{PostId}")]
        public async Task<IActionResult> DeletePostById(int? PostId)
        {
            if (PostId == null) return BadRequest("PostId can not be empty");

            Post? post = await postsRepository.GetPostById(PostId);
            if (post == null)
            {
                return NotFound("Post not found");
            }

            await postsRepository.DeletePostAsync(post);
            return NoContent();
        }

    }
}
