using Eravlol.UserWebApi.Data.Models;
using Eravol.WebApi.Data.Models;
using Eravol.WebApi.Repositories.Certificates;
using Eravol.WebApi.Repositories.Images;
using Eravol.WebApi.ViewModels.Certificates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace Eravol.WebApi.Controllers.Certificates
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificatesController : ControllerBase
    {
        #region
        private const string USER_CONTENT_FOLDER_NAME = "user-content";
        #endregion

        private readonly ICertiticateRepository certiticateRepository;
        private readonly IFileStorageService fileStorageService;

        public CertificatesController(
            ICertiticateRepository certiticateRepository,
            IFileStorageService fileStorageService
        )
        {
            this.certiticateRepository = certiticateRepository;
            this.fileStorageService = fileStorageService;
        }

        /// <summary>
        /// Create Certificate
        /// </summary>
        /// <param name="createRequest"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateCertificate([FromForm] CreateCertificateViewModel createRequest)
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

            //Create new Certificate object
            Certificate certificate = new Certificate()
            {
                CertificateTitle = createRequest.CertificateTitle,
                CertificateDate = createRequest.CertificateDate,
                CertificateImageName = fileStorageService.GetUniqueFileName(createRequest.CertificateImageName),
                CertificateImageSize = createRequest.CertificateImageSize,
                IsCertificatePublic = true,
                UserId= UserId
            };

            //Create User Certificate into database
            await certiticateRepository.CreateCertificate(certificate);

            //Save file into folder
            await SaveFile(createRequest.CertificateImage, certificate.CertificateImageName);

            return Ok(certificate);
        }


        /// <summary>
        /// Save image file into folder server
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private async Task SaveFile(IFormFile? file, string? fileName)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            await fileStorageService.SaveFileAsync(file.OpenReadStream(), fileName);
        }
    }
}
