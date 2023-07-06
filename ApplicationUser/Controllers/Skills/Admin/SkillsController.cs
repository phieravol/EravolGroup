using Eravlol.UserWebApi.Data.Models;
using Eravol.UserWebApi.Data.Models;
using Eravol.UserWebApi.Repository.Skills;
using Eravol.UserWebApi.ViewModels.Skills;
using Eravol.WebApi.Data.Models;
using Eravol.WebApi.ViewModels.Base;
using Eravol.WebApi.ViewModels.PostStatuses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;

namespace Eravol.WebApi.Controllers.Skills.Admin
{
    [Route("api/Admin/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillRepository _skillRepository;
        private readonly UserManager<AppUser> _userManager;
        public SkillsController(ISkillRepository skillRepository, UserManager<AppUser> userManager)
        {
            _skillRepository = skillRepository;
            _userManager = userManager;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetSkills([FromQuery] PagingRequestBase<Skill> request)
        {
            request.SearchTerm = WebUtility.UrlDecode(request.SearchTerm);
            string? UserIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //If User not login then return message
            if (string.IsNullOrEmpty(UserIdStr))
            {
                return BadRequest("User Can't found in the session");
            }
            List<Skill> skills = await _skillRepository.GetSkillSearchPaging(request);

            request.TotalPages = (int)Math.Ceiling(skills.Count() / (double)request.PageSize);

            skills = skills.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize).ToList();
            request.Items = skills;
            return Ok(request);
        }

        [HttpGet("skillId")]
        public async Task<IActionResult> GetSkillById(int? skillId)
        {
            if (skillId is null) return NotFound("Skill Id not found");
            Skill? skill = await _skillRepository.GetSkillByIdAsync(skillId);

            if (skillId is null)
            {
                return NotFound("Post Status not found");
            }
            return Ok(skill);
        }
        [HttpPost]
        public async Task<IActionResult> CreateSkill([FromForm] SkillViewModel skills)
        {
            skills.SkillName = WebUtility.UrlDecode(skills.SkillName);
            //skills.isPublic = WebUtility.UrlDecode(skills.isPublic.Value);
            await _skillRepository.CreateSkillAsync(skills);
            return Created("./Index", skills);
        }
        [HttpPut("{skillId}")]
        public async Task<IActionResult> UpdateSkill(int? skillId, [FromForm] SkillViewModel? skill)
        {
            ////Find UserId by claims
            //string? UserIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            ////return error message if userID not found
            //if (string.IsNullOrEmpty(UserIdStr))
            //{
            //    return NotFound("User is not login, please login");
            //}
            if (skillId is null) return NotFound("Skill Id not found");
            if (skillId is null) return NotFound("Skill is Empty");

            //get category by id
            Skill? currentSkill = await _skillRepository.GetSkillByIdAsync(skillId);
            if (currentSkill is null)
            {
                return NotFound("Skill not found");
            }
            currentSkill.SkillName = skill.SkillName;
            currentSkill.isPublic = skill.isPublic;

            //update category with image
            await _skillRepository.UpdateSkillAsync(currentSkill);

            return NoContent();
        }
        [HttpDelete("{skillId}")]
        public async Task<IActionResult> DeletePostStatus(int? skillId)
        {
            if (skillId is null) return NotFound("Skill Id not found");

            Skill? currentSkill = await _skillRepository.GetSkillByIdAsync(skillId);

            if (currentSkill is null)
            {
                return NotFound("Skill not found");
            }
            await _skillRepository.DeleteSkillAsync(currentSkill);

            return NoContent();
        }

    }
}
