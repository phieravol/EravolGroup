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

    }
}
