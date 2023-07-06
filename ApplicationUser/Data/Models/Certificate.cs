using Eravlol.UserWebApi.Data.Models;

namespace Eravol.WebApi.Data.Models
{
	public class Certificate
	{
		#region Fields
		public int CertificateId { get; set; }
		public string? CertificateTitle { get; set; }
		public DateTime? CertificateDate { get; set; }
		public bool IsCertificatePublic { get; set; }
		public string? CertificateImageName { get; set; }
		public long? CertificateImageSize { get; set; }
		public string? CertificateImagePath { get; set; }
		#endregion

		#region Foreignkey
		public Guid UserId { get; set; }
		#endregion

		#region References
		public virtual AppUser AppUser { get; set; }
		#endregion
	}
}
