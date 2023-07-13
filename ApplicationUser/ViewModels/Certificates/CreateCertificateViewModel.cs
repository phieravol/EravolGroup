namespace Eravol.WebApi.ViewModels.Certificates
{
    public class CreateCertificateViewModel
    {
        public string? CertificateTitle { get; set; }
        public DateTime? CertificateDate { get; set; }
        public string? CertificateImageName { get; set; }
        public long? CertificateImageSize { get; set; }
        public string? CertificateImagePath { get; set; }
        public Guid UserId { get; set; }
        public IFormFile? CertificateImage { get; set; }
    }
}
