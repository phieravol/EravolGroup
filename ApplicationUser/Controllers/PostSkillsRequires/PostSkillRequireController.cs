using Eravol.UserWebApi.Data.Models;
using Eravol.WebApi.Data.Models;
using Eravol.WebApi.Repositories.PostSkills;
using Eravol.WebApi.ViewModels.PostSkillRequires;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Eravol.WebApi.Controllers.PostSkills
{
    [Route("api/[controller]")]
    [ApiController]

    public class PostSkillRequireController : ControllerBase
    {
        #region DI Services
        private readonly IPostSkillsRepository skillRequireRepository;
        #endregion

        #region Constructor
        public PostSkillRequireController(IPostSkillsRepository skillRequireRepository)
        {
            this.skillRequireRepository = skillRequireRepository;
        }
        #endregion

        /// <summary>
        /// Get Post skill require by post ID
        /// </summary>
        /// <param name="PostId"></param>
        /// <returns></returns>
        [HttpGet("{PostId}")]
        public async Task<IActionResult> GetSkillRequireByPostId(int? PostId)
        {
            if (PostId == null)
            {
                return BadRequest("Post Id can not null");
            }

            List<PostSkillRequireViewModel> postSkills = await skillRequireRepository.GetSkillRequireByPostId(PostId);
            return Ok(postSkills);
        }

        /// <summary>
        /// Get Skill Require by search term
        /// </summary>
        /// <param name="SearchTerm"></param>
        /// <returns></returns>
		[HttpGet("SearchTerm")]
		public async Task<IActionResult> GetSkillRequireBySearchTerm(string? SearchTerm)
		{
            List<PostSkillRequireViewModel>? Skills = skillRequireRepository.GetSkillRequireBySearchTerm(SearchTerm);
			return Ok(Skills);
		}

		/// <summary>
		/// Create PostSkillRequires by List of SkillRequires from Ajax
		/// </summary>
		/// <param name="skillRequires"></param>
		/// <returns></returns>
		[HttpPost]
        public async Task<IActionResult> CreatePostSkillRequires(List<CreateSkillRequiresRequest>? skillRequires)
        {
            if (skillRequires == null || skillRequires.Count == 0)
            {
                return Ok(skillRequires);
            }
            List<PostSkillRequired> postSkills = new List<PostSkillRequired>();

            foreach (var item in skillRequires)
            {
                PostSkillRequired postSkill = new PostSkillRequired()
                {
                    SkillId = item.SkillId,
                    PostId = item.PostId
                };

                postSkills.Add(postSkill);
            }
            await skillRequireRepository.CreateSkillsRequireAsync(postSkills);

            return Ok(skillRequires);
        }

        [HttpPost("SpecifiedPost")]
        public async Task<IActionResult> CreateSpecifiedSkillRequires(CreateSkillRequiresRequest? skillRequire)
        {
            if (skillRequire == null)
            {
                return BadRequest("Skill require data empty");
            }

            PostSkillRequired skillRequired = new PostSkillRequired()
            {
                SkillId = skillRequire.SkillId,
                PostId = skillRequire.PostId
            };

            await skillRequireRepository.CreateSpecifySkillRequireAsync(skillRequired);
            return NoContent();
        }


        /// <summary>
        /// UpdateServiceRequest Post Skill Require by Ajax
        /// </summary>
        /// <param name="skillRequireId"></param>
        /// <param name="skillRequires"></param>
        /// <returns></returns>
        [HttpPut("{skillRequireId}")]
        public async Task<IActionResult> UpdatePostSkillRequire(int? skillRequireId, CreateSkillRequiresRequest skillRequire)
        {
            //if PostSkill Require is null then return error message
            if (skillRequireId==null)
            {
                return BadRequest("Id of Post Skill Require is empty");
            }

            //get old PostSkillRequire from Db
            PostSkillRequired postSkillRequired = await skillRequireRepository.GetSkillRequireById(skillRequireId);

            //if postSkillRequired not found then return error message
            if (postSkillRequired == null)
            {
                return NotFound("PostSkillRequire not found");
            }

            //set new value for old PostSkillRequired
            postSkillRequired.SkillId = skillRequire.SkillId;

            //process update PostSkillRequire
            await skillRequireRepository.UpdateSkillRequire(postSkillRequired);
            return Ok(skillRequire);
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePostSkillRequire([FromQuery] int? skillRequireId, [FromQuery] int? postId)
        {
            if (skillRequireId==null) 
            { 
                return BadRequest("Please enter post skill require id");
            }

            PostSkillRequired? skillRequired = await skillRequireRepository.GetSpecificSkillRequire(skillRequireId, postId);
            if (skillRequired == null)
            {
                return NotFound("This skill require not found");
            }

            await skillRequireRepository.DeleteSkillRequire(skillRequired);

            return Ok(skillRequired);
        }

    }
}
