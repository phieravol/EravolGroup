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
				.IsRequired()
				.HasMaxLength(1024);
			
		}
	}
}
