using Eravol.WebApi.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eravol.WebApi.Data.Configuration
{
	public class ServiceImageConfiguration : IEntityTypeConfiguration<ServiceImage>
	{
		public void Configure(EntityTypeBuilder<ServiceImage> builder)
		{
			#region configFields
			builder.ToTable("ServiceImage");
			builder.HasKey(x => x.ServiceImgageId);
			builder.Property(x => x.ServiceImagePath).IsRequired()
				.HasMaxLength(2000);
			builder.Property(x => x.isThumbnail).IsRequired();
			builder.Property(x=>x.DateCreated).IsRequired(false);
			builder.Property(x=>x.ServiceImageSize).IsRequired(false);
			#endregion
		}
	}
}
