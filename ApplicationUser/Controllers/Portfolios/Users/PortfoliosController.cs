using Eravol.UserWebApi.Data.Models;
using Eravol.WebApi.Data.Models;
using Eravol.WebApi.Repositories.Images;
using Eravol.WebApi.Repositories.Portfolios.Users;
using Eravol.WebApi.Repositories.UserImages;
using Eravol.WebApi.ViewModels.Experiences;
using Eravol.WebApi.ViewModels.Portfolios.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace Eravol.WebApi.Controllers.Portfolios.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfoliosController : ControllerBase
    {
        #region
        private const string USER_CONTENT_FOLDER_NAME = "user-content";
        #endregion

        private readonly IPortfolioRepository portfolioRepository;
        private readonly IFileStorageService fileStorageService;

        public PortfoliosController(
            IPortfolioRepository portfolioRepository,
            IFileStorageService fileStorageService
        )
        {
            this.portfolioRepository = portfolioRepository;
            this.fileStorageService = fileStorageService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolios()
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

            //Get portfolio list from database
            List<Portfolio>? portfolios = await portfolioRepository.GetCurrentUserPortfolios(UserId);
            return Ok(portfolios);
        }


        /// <summary>
        /// Create user Portfolio into database
        /// </summary>
        /// <param name="createRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateUserPortfolio([FromForm] CreatePortfolioViewModel? createRequest)
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

            Portfolio portfolio = new Portfolio()
            {
                PortfolioTitle = createRequest.PortfolioTitle,
                PortfolioDescription = createRequest.PortfolioDescription,
                PortfolioUrl = createRequest.PortfolioUrl,
                IsPortfolioPublic = true,
                PortfolioImageSize = createRequest.PortfolioImage?.Length,
                UserId = UserId
            };

            portfolio.PortfolioImageName = await SaveFile(createRequest.PortfolioImage);
            portfolio.PortfolioImagePath = "/" + USER_CONTENT_FOLDER_NAME + "/" + portfolio.PortfolioImageName;

            await portfolioRepository.AddUserPortfolioToDb(portfolio);
            return Ok(portfolio);
        }

        /// <summary>
        /// Delete User Portfolio by UserId
        /// </summary>
        /// <param name="portfolioId"></param>
        /// <returns></returns>
        [HttpDelete("{portfolioId}")]
        [Authorize]
        public async Task<IActionResult> DeleteUserPortfolio(int? portfolioId)
        {
            //check if porfolio Id is null
            if (portfolioId == null)
            {
                return BadRequest("Portfolio Id is empty");
            }

            //Get portfolio by id
            Portfolio? portfolio = await portfolioRepository.GetUserPortfolioById(portfolioId);

            //Return message when portfolio not found
            if (portfolio == null)
            {
                return NotFound("Portfolio not found");
            }

            //Delete user portfolio in folder
            string? portfolioImg = portfolio.PortfolioImageName;
            if (!string.IsNullOrWhiteSpace(portfolioImg))
            {
                await fileStorageService.DeleteFileAsync(portfolioImg);
            }

            //Delete User portfolio object in database
            await portfolioRepository.DeleteUserPortfolio(portfolio);

            return Ok();
        }


        /// <summary>
        /// Save image file into folder server
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private async Task<string?> SaveFile(IFormFile? file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await fileStorageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
    }

}
