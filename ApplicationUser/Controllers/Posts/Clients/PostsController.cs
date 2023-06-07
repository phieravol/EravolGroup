using Eravlol.UserWebApi.Data.Models;
using Eravol.WebApi.Data.Models;
using Eravol.WebApi.Repositories.Posts.Clients;
using Eravol.WebApi.ViewModels.Categories;
using Eravol.WebApi.ViewModels.Posts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace Eravol.WebApi.Controllers.Posts.Clients
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {

        private readonly IClientsPostRepository postsRepository;
        private readonly UserManager<AppUser> userManager;

        public PostsController(
            IClientsPostRepository postsRepository,
            UserManager<AppUser> userManager
        )
        {
            this.postsRepository = postsRepository;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromForm] CreatePostRequest request)
        {
            string UserIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            AppUser user = await userManager.FindByIdAsync(UserIdStr);

            if (UserIdStr==null)
            {
                return BadRequest("Username can not null");
            }
            Post post = new Post()
            {
                PostTitle = request.PostTitle,
                PostDetails= request.PostDetails,
                PostStatusId = request.PostStatusId,
                PostedDate = DateTime.Now,
                CategoryId= request.CategoryId,
                Budget = request.Budget,
                ExpirationDate= request.ExpirationDate,
                LevelRequired = request.LevelRequired,
                UserId = Guid.Parse(UserIdStr),
            };
            await postsRepository.CreatePostAsync(post);
            
            return Created("./Index", request);
        }
    }
}
