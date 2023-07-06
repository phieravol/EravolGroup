using Eravol.WebApi.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eravol.WebApi.Data.Configuration
{
	public class ExperienceConfiguration : IEntityTypeConfiguration<Experience>
	{
		public void Configure(EntityTypeBuilder<Experience> builder)
		{
			builder.ToTable("Experience");
			builder.HasKey(x => x.ExperienceId);
			builder.Property(x => x.CompanyTitle).IsRequired().HasMaxLength(200);
			builder.Property(x => x.JobDescription).IsRequired(false).HasMaxLength(1500);
			builder.Property(x => x.Position).IsRequired().HasMaxLength(200);
			builder.Property(x => x.StartingDate).IsRequired();
			builder.Property(x => x.EndingDate).IsRequired(false);
		}
	}
}
