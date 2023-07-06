using Eravlol.UserWebApi.Data.Models;
using Eravol.WebApi.Data.Models;
using Eravol.WebApi.Repositories.PostStatuses.Clients;
using Eravol.WebApi.ViewModels.Base;
using Eravol.WebApi.ViewModels.PostStatuses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace Eravol.WebApi.Controllers.PostStatuses.Clients
{
    [Route("api/Admin/[controller]")]
    [ApiController]
    public class PostStatusesController : Controller
    {
        private readonly IPostStatusesRepository _postStatusesRepository;
        private readonly UserManager<AppUser> _userManager;

        public PostStatusesController(IPostStatusesRepository postStatusesRepository, UserManager<AppUser> userManager)
        {
            _postStatusesRepository = postStatusesRepository;
            _userManager = userManager;
        }
        /// <summary>
        /// Get all post status to display for client
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetPostStatus([FromQuery] PagingRequestBase<PostStatus> request)
        {
            request.SearchTerm = WebUtility.UrlDecode(request.SearchTerm);
            string? UserIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //If User not login then return message
            if (string.IsNullOrEmpty(UserIdStr))
            {
                return BadRequest("User Can't found in the session");
            }
            List<PostStatus> postStatuses = await _postStatusesRepository.GetPostStatusSearchPaging(request);

            request.TotalPages = (int)Math.Ceiling(postStatuses.Count() / (double)request.PageSize);

            postStatuses = postStatuses.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize).ToList();
            request.Items = postStatuses;
            return Ok(request);
        }

        [HttpGet("PostStatusId")]
        public async Task<IActionResult> GetPostStatusById(int? PostStatusId)
        {
            if (PostStatusId is null) return NotFound("Post Status Id not found");
            PostStatus? postStatus = await _postStatusesRepository.GetPostStatusByIdAsync(PostStatusId);

            if (postStatus is null)
            {
                return NotFound("Post Status not found");
            }
            return Ok(postStatus);
        }
        [HttpPost]
        public async Task<IActionResult> CreatePostStatus([FromForm] UpdatePostStatusRequest postStatuses)
        {
            postStatuses.PostStatusName = WebUtility.UrlDecode(postStatuses.PostStatusName);
            postStatuses.PostStatusDesc = WebUtility.UrlDecode(postStatuses.PostStatusDesc);
            await _postStatusesRepository.CreatePostStatusAsync(postStatuses);
            return Created("./Index", postStatuses);
        }
        [HttpPut("{PostStatusId}")]
        public async Task<IActionResult> UpdatePostStatus(int? PostStatusId, [FromForm] UpdatePostStatusRequest? postStatus)
        {
            ////Find UserId by claims
            //string? UserIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            ////return error message if userID not found
            //if (string.IsNullOrEmpty(UserIdStr))
            //{
            //    return NotFound("User is not login, please login");
            //}
            if (PostStatusId is null) return NotFound("Post Status Id not found");
            if (PostStatusId is null) return NotFound("Post Status is Empty");

            //get category by id
            PostStatus? currentPostStatus = await _postStatusesRepository.GetPostStatusByIdAsync(PostStatusId);
            if (currentPostStatus is null)
            {
                return NotFound("Post status not found");
            }
            currentPostStatus.PostStatusName = postStatus.PostStatusName;
            currentPostStatus.PostStatusDesc = postStatus.PostStatusDesc;

            //update category with image
            await _postStatusesRepository.UpdatePostStatusAsync(currentPostStatus);

            return NoContent();
        }
        [HttpDelete("{PostStatusId}")]
        public async Task<IActionResult> DeletePostStatus(int? PostStatusId)
        {
            if (PostStatusId is null) return NotFound("Post Status Id not found");

            PostStatus? currentPostStatus = await _postStatusesRepository.GetPostStatusByIdAsync(PostStatusId);

            if (currentPostStatus is null)
            {
                return NotFound("Post Status not found");
            }
            await _postStatusesRepository.DeletePostStatusAsync(currentPostStatus);

            return NoContent();
        }
    }
}
