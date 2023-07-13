using Eravol.WebApi.Data.Models;

namespace Eravol.WebApi.Repositories.Certificates
{
    public interface ICertiticateRepository
    {
        Task CreateCertificate(Certificate certificate);
        Task DeleteCertificate(Certificate certificate);
        Task<Certificate?> GetCertificatesById(int? certificateId);
        Task<List<Certificate>?> GetCertificatesByUserId(Guid userId);
        Task UpdateCertificate(Certificate certificate);
    }
}
