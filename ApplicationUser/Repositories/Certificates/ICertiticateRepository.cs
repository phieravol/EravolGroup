using Eravol.WebApi.Data.Models;

namespace Eravol.WebApi.Repositories.Certificates
{
    public interface ICertiticateRepository
    {
        Task CreateCertificate(Certificate certificate);
    }
}
