using Eravol.WebApi.Data.Models;
using Eravol.WebApi.Repositories.UserSkills;
using Eravol.WebApi.ViewModels.UserSkills;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Eravol.WebApi.Controllers.UserSkills.Public
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicUserSkillsController : ControllerBase
    {
        #region Dependency Injection Services
        private readonly IPublicUserSkillRepository userSkillRepository;
        #endregion

        #region Constructor
        public PublicUserSkillsController(IPublicUserSkillRepository publicUserSkill)
        {
            this.userSkillRepository = publicUserSkill;
        }
        #endregion

        [HttpGet("MySkills")]
        [Authorize]
        public async Task<IActionResult> GetMyUserSkills()
        {
            //Get UserId by claims in token
            string? UserIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            /* Check UserId is null or not null */
            if (string.IsNullOrWhiteSpace(UserIdStr))
            {
                return BadRequest("UserId can not be null");
            }

            //Convert ID from string to GUID
            Guid UserId = Guid.Parse(UserIdStr);

            //Get list UserSkill from database
            List<UserSkillViewModel>? UserSkills = await userSkillRepository.GetMyUserSkillsByUserId(UserId);
            return Ok(UserSkills);
        }


        /// <summary>
        /// Create a new UserSkill to database
        /// </summary>
        /// <param name="userSkillVM">UserSkillViewModel</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddUserSkillToDb(CreateUserSkillViewModel userSkillVM)
        {
            //Get UserId by claims in token
            string? UserIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            /* Check UserId is null or not null */
            if (string.IsNullOrWhiteSpace(UserIdStr))
            {
                return BadRequest("UserId can not be null");
            }

            //Convert ID from string to GUID
            Guid UserId = Guid.Parse(UserIdStr);

            //Check UserSkill Exist or not
            UserSkill existUserSkill = userSkillRepository.GetSpecificUserSkill(userSkillVM.SkillId, UserId);
            if (existUserSkill != null)
            {
                return BadRequest("Current UserSkill is Existed in database.");
            }

            //Create new UserSkill
            UserSkill? userSkill = new UserSkill()
            {
                UserId = UserId,
                SkillId = userSkillVM.SkillId,
                Score = userSkillVM.Score,
                IsVerified = false,
            };

            //Add UserSkill to database
            await userSkillRepository.AddUserSkillToDb(userSkill);
            
            return Ok(userSkillVM);
        }

    }
}
