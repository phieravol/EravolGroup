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
        /// Get List Certificate of current User
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCertificates()
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

            List<Certificate>? certificates = await certiticateRepository.GetCertificatesByUserId(UserId);
            return Ok(certificates);
        }

        /// <summary>
        /// Get specific certificate
        /// </summary>
        /// <param name="certificateId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{certificateId}")]
        public async Task<IActionResult> GetCertificateById(int? certificateId)
        {
            if (certificateId == null) 
            {
                return BadRequest("Certificate Id can not empty");
            }

            Certificate? certificate = await certiticateRepository.GetCertificatesById(certificateId);

            if (certificate == null)
            {
                return NotFound("Certificate not found!");
            }
            return Ok(certificate);
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
            certificate.CertificateImagePath = "/" + USER_CONTENT_FOLDER_NAME + "/" + certificate.CertificateImageName;

            //Create User Certificate into database
            await certiticateRepository.CreateCertificate(certificate);

            //Save file into folder
            await SaveFile(createRequest.CertificateImage, certificate.CertificateImageName);

            return Ok(certificate);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateCertificate([FromForm] UpdateCertificateViewModel updateRequest)
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

            //Get Certificate by Id
            Certificate? certificate = await certiticateRepository.GetCertificatesById(updateRequest.CertificateId);

            //Return error message if certificate is empty
            if (certificate == null)
            {
                return NotFound("Certificate is not found");
            }

            //Remove old image
            string? certificateImg = certificate.CertificateImageName;
            if (!string.IsNullOrWhiteSpace(certificateImg))
            {
                await fileStorageService.DeleteFileAsync(certificateImg);
            }

            //Update certificate properties
            certificate.CertificateTitle = updateRequest.CertificateTitle;
            certificate.CertificateDate = updateRequest.CertificateDate;
            certificate.CertificateImageName = fileStorageService.GetUniqueFileName(updateRequest.CertificateImageName);
            certificate.CertificateImageSize = certificate.CertificateImageSize;
            certificate.CertificateImagePath = "/" + USER_CONTENT_FOLDER_NAME + "/" + certificate.CertificateImageName;
            certificate.UserId = UserId;

            await certiticateRepository.UpdateCertificate(certificate);

            await SaveFile(updateRequest.CertificateImage, certificate.CertificateImageName);
            return Ok(certificate);
        }

        [Authorize]
        [HttpDelete("{certificateId}")]
        public async Task<IActionResult> UpdateCertificate(int? certificateId)
        {
            if (certificateId == null)
            {
                return BadRequest("Certificate Id is required!");
            }

            //Get Certificate by Id
            Certificate? certificate = await certiticateRepository.GetCertificatesById(certificateId);
            if (certificate == null)
            {
                return NotFound("Certificate not found!");
            }

            await certiticateRepository.DeleteCertificate(certificate);
            return Ok();
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
