using Eravol.UserWebApi.Data.Models;
using Eravol.UserWebApi.Repository.Skills;
using Eravol.UserWebApi.ViewModels.Skills;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Eravol.UserWebApi.Controllers.Skills
{
    [Route("api/user/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillRepository skillRepository;
        public SkillsController(ISkillRepository skillRepository)
        {
            this.skillRepository = skillRepository;
        }

        [HttpGet("{username}")]
        [Authorize]
        public IActionResult GetUserSkills(string username)
        {
            //Get all Skill by Username
            List<Skill> userSkills = skillRepository.GetSkillsByUsername(username);
            return Ok(userSkills);
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateUserSkills(SkillViewModel request)
        {
            string? rawId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Guid UserId = Guid.Parse(rawId);
            Skill skill = new Skill()
            {
                SkillName = request.SkillName,
                Score = request.Score,
                UserId = UserId
            };
            if (!ModelState.IsValid)
            {
                return BadRequest("Some fields Skill Request is invalid!");
            }

            skillRepository.CreateUserSkill(skill);
            return Ok(skill);
        }

        [HttpPut]
        [Authorize]
        public IActionResult UpdateUserSkill(SkillViewModel? request)
        {
            if (request.Id == null)
            {
                return BadRequest("Parameter skill ID cannot be null!");
            }
            Skill skill = skillRepository.getSkillById(request.Id);

            if (skill == null)
            {
                return NotFound($"Can not find skill!");
            }
            skill.SkillName = request.SkillName;
            skill.Score = request.Score;
            skillRepository.UpdateSkillAsync(skill);

            return Ok();
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteUserSkill(int? skillId)
        {
            if (skillId == null)
            {
                return BadRequest("Parmaeter ");
            }

            Skill skill = skillRepository.getSkillById(skillId);

            if (skill == null)
            {
                return NotFound($"Can not find skill!");
            }

            await skillRepository.RemoveSkillAsync(skill);
            return Ok(skill);
        }
    }
}
