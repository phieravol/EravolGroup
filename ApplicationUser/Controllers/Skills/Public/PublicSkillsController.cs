using Eravol.UserWebApi.Data.Models;
using Eravol.WebApi.Repositories.Skills.Public;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eravol.WebApi.Controllers.Skills.Public
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicSkillsController : ControllerBase
    {
        #region Dependency Injection Services
        private readonly IPublicSkillRepository skillRepository;
        #endregion

        #region Constructor
        public PublicSkillsController(IPublicSkillRepository skillRepository)
        {
            this.skillRepository = skillRepository;
        }
        #endregion

        /// <summary>
        /// Get all public skills
        /// </summary>
        /// <returns></returns>
        [HttpGet("PublicSkills")]
        public async Task<IActionResult> GetPublicSkills()
        {
            List<Skill> Skills = skillRepository.GetAllPublicSkills();
            return Ok(Skills);
        }

    }
}
