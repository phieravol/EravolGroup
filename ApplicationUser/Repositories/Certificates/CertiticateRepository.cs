using Eravol.UserWebApi.Data;
using Eravol.WebApi.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Eravol.WebApi.Repositories.Certificates
{
    public class CertiticateRepository : ICertiticateRepository
    {
        private readonly EravolUserWebApiContext context;

        public CertiticateRepository(EravolUserWebApiContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Create Certificate into database
        /// </summary>
        /// <param name="certificate"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task CreateCertificate(Certificate certificate)
        {
            try
            {
                context.Certificates.Add(certificate);
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Delete Certificate
        /// </summary>
        /// <param name="certificate"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task DeleteCertificate(Certificate certificate)
        {
            try
            {
                context.Certificates.Remove(certificate);
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Get specific certificate
        /// </summary>
        /// <param name="certificateId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Certificate?> GetCertificatesById(int? certificateId)
        {
            try
            {
                Certificate? certificate = await context.Certificates.FindAsync(certificateId);
                return certificate;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Get Certificate by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<Certificate>?> GetCertificatesByUserId(Guid userId)
        {
            try
            {
                List<Certificate>? certificates = await context.Certificates
                    .Where(x =>x.UserId == userId)
                    .ToListAsync();
                return certificates;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Update Certificate
        /// </summary>
        /// <param name="certificate"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task UpdateCertificate(Certificate certificate)
        {
            try
            {
                context.Certificates.Update(certificate);
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
