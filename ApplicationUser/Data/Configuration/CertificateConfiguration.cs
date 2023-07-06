using Eravol.WebApi.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eravol.WebApi.Data.Configuration
{
	public class CertificateConfiguration : IEntityTypeConfiguration<Certificate>
	{
		public void Configure(EntityTypeBuilder<Certificate> builder)
		{
			builder.ToTable("Certificate");
			builder.HasKey(x => x.CertificateId);
			builder.Property(x => x.CertificateTitle).IsRequired().HasMaxLength(256);
			builder.Property(x => x.CertificateDate).IsRequired(false);
			builder.Property(x => x.IsCertificatePublic).IsRequired();
			builder.Property(x => x.CertificateImageName).IsRequired(false).HasMaxLength(256);
			builder.Property(x => x.CertificateImageSize).IsRequired(false);
			builder.Property(x => x.CertificateImagePath).IsRequired(false).HasMaxLength(1000);
		}
	}
}
