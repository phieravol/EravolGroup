using Eravol.WebApi.Data.Models;
using Eravol.WebApi.Repositories.Experiences;
using Eravol.WebApi.ViewModels.Experiences;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Eravol.WebApi.Controllers.Experiences
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExperiencesController : ControllerBase
    {
        private readonly IUserExperiencesRepository experiencesRepository;

        public ExperiencesController(IUserExperiencesRepository experiencesRepository)
        {
            this.experiencesRepository = experiencesRepository;
        }

        /// <summary>
        /// Create User Experience into database
        /// </summary>
        /// <param name="createRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateUserExperience(CreateExperienceViewModel? createRequest)
        {
            if (createRequest == null) { return BadRequest("Create User Experience request is empty!"); }

            //Get UserId by claims in token
            string? UserIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            /* Check UserId is null or not null */
            if (string.IsNullOrWhiteSpace(UserIdStr))
            {
                return BadRequest("UserId can not be null");
            }

            //Convert ID from string to GUID
            Guid UserId = Guid.Parse(UserIdStr);

            //Init a new Experience object
            Experience experience = new Experience()
            {
                UserId = UserId,
                CompanyTitle = createRequest.CompanyTitle,
                Position = createRequest.Position,
                StartingDate = createRequest.StartingDate,
                EndingDate = createRequest.EndingDate,
                JobDescription = createRequest.JobDescription,
            };

            //Create Experience into database
            await experiencesRepository.CreateUserExperience(experience);
            return Ok(experience);
        }

        /// <summary>
        /// Get User Experience by current UserId
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetMyExperience()
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

            //Get my experience
            List<Experience>? experiences = experiencesRepository.GetMyExperiences(UserId);
            return Ok(experiences);
        }

        /// <summary>
        /// Update User Experience
        /// </summary>
        /// <param name="updateRequest"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateUserExperience(UpdateExperienceViewModel? updateRequest)
        {
            if (updateRequest == null) { 
                return BadRequest("Create User Experience request is empty!"); 
            }

            //Get UserId by claims in token
            string? UserIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            /* Check UserId is null or not null */
            if (string.IsNullOrWhiteSpace(UserIdStr))
            {
                return BadRequest("UserId can not be null");
            }

            //Convert ID from string to GUID
            Guid UserId = Guid.Parse(UserIdStr);

            //Get User Experience by Id
            Experience? currentExperience = await experiencesRepository.GetUserExperienceById(updateRequest.ExperienceId);
            if (currentExperience == null)
            {
                return BadRequest("User Experience not found!");
            }

            currentExperience.CompanyTitle = updateRequest.CompanyTitle;
            currentExperience.StartingDate = updateRequest.StartingDate;
            currentExperience.EndingDate = updateRequest?.EndingDate;
            currentExperience.JobDescription = updateRequest?.JobDescription;
            currentExperience.Position = updateRequest.Position;

            //Update User Experience
            await experiencesRepository.UpdateUserExperience(currentExperience);
            return Ok(currentExperience);
        }

        [HttpDelete("{experienceId}")]
        [Authorize]
        public async Task<IActionResult> DeleteUserExperience(int experienceId)
        {
            //Get User Experience by Id
            Experience? currentExperience = await experiencesRepository.GetUserExperienceById(experienceId);
            if (currentExperience == null)
            {
                return BadRequest("User Experience not found!");
            }

            //Delete User Experience
            await experiencesRepository.DeleteUserExperience(currentExperience);
            return Ok();
        }

    }
}
