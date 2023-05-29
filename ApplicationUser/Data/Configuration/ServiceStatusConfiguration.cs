using Eravol.WebApi.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eravol.WebApi.Data.Configuration
{
	public class ServiceStatusConfiguration : IEntityTypeConfiguration<ServiceStatus>
	{
		public void Configure(EntityTypeBuilder<ServiceStatus> builder)
		{
			#region ConfigureFields
			builder.ToTable("ServiceStatus");
			builder.HasKey(x => x.ServiceStatusId);
			builder.Property(x=> x.ServiceStatusName).IsRequired()
				.HasMaxLength(50);
			builder.Property(x=>x.ServiceStatusDesc).IsRequired(false)
				.HasMaxLength(500);
			#endregion

			#region ConfigureRelationships
			builder.HasMany(x => x.Services).WithOne(x => x.ServiceStatus)
				.HasForeignKey(x => x.ServiceStatusId);
			#endregion
		}
	}
}
