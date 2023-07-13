using Eravol.UserWebApi.Data;
using Eravol.WebApi.Data.Models;

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
    }
}
