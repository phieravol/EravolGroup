using Eravol.UserWebApi.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eravol.UserWebApi.Data.Configuration
{
	public class UserImageConfiguration : IEntityTypeConfiguration<UserImage>
	{
		public void Configure(EntityTypeBuilder<UserImage> builder)
		{
			builder.ToTable("UserImage");
			builder.HasKey(x => x.ImgageId);
			builder.Property(x => x.UserImagePath)
				.IsRequired(false)
				.HasMaxLength(1024);
			builder.Property(x => x.UserImageName)
				.IsRequired(false)
				.HasMaxLength(256);
			
		}
	}
}
