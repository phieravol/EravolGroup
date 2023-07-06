using Eravol.WebApi.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eravol.WebApi.Data.Configuration
{
	public class PortfolioConfiguration : IEntityTypeConfiguration<Portfolio>
	{
		public void Configure(EntityTypeBuilder<Portfolio> builder)
		{
			builder.ToTable("Portfolio");
			builder.HasKey(x => x.PortfolioId);
			builder.Property(x => x.PortfolioTitle).IsRequired().HasMaxLength(256);
			builder.Property(x => x.PortfolioDescription).IsRequired(false).HasMaxLength(2500);
			builder.Property(x => x.PortfolioImageName).IsRequired().HasMaxLength(256);
			builder.Property(x => x.PortfolioImagePath).IsRequired().HasMaxLength(1024);
			builder.Property(x => x.PortfolioImageSize).IsRequired();
			builder.Property(x => x.PortfolioUrl).IsRequired(false);
			builder.Property(x => x.IsPortfolioPublic).IsRequired();
		}
	}
}
