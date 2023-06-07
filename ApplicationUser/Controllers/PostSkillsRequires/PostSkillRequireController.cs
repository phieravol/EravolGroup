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
        private readonly IPostSkillsRepository skillRequireRepository;

        public PostSkillRequireController(IPostSkillsRepository skillRequireRepository)
        {
            this.skillRequireRepository = skillRequireRepository;
        }

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

    }
}
