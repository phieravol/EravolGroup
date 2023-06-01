using Eravol.WebApi.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eravol.WebApi.Data.Configuration
{
	public class ServiceConfiguration : IEntityTypeConfiguration<Service>
	{
		public void Configure(EntityTypeBuilder<Service> builder)
		{
			#region ConfigFields
			builder.ToTable("Service");
			builder.HasKey(x => x.ServiceCode);
			builder.Property(x => x.ServiceCode).HasMaxLength(50);
			builder.Property(x=>x.ServiceTitle).IsRequired()
				.HasMaxLength(800);
			builder.Property(x => x.ServiceIntro).IsRequired(false).
				HasMaxLength(2000);
			builder.Property(x => x.ServiceDetails).IsRequired(false);
			builder.Property(x => x.ServiceAuthor).HasMaxLength(100);
			builder.Property(x=>x.TotalClients).IsRequired(false);
			builder.Property(x=>x.TotalStars).IsRequired(false);
			#endregion

			#region ConfigRelationships
			builder.HasMany(x=>x.ServiceImages).WithOne(x=>x.Service)
				.HasForeignKey(x=>x.ServiceCode);
			#endregion
		}
	}
}
