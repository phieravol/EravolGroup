using Eravlol.UserWebApi.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eravol.UserWebApi.Data.Configuration
{
	public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
	{
		public void Configure(EntityTypeBuilder<AppUser> builder)
		{
			builder.Property(x => x.FirstName)
				.IsRequired()
				.HasMaxLength(50);
			builder.Property(x => x.LastName)
				.IsRequired()
				.HasMaxLength(50);
			builder.Property(x => x.Tagline)
				.IsRequired(false)
				.HasMaxLength(500);
			builder.Property(x => x.Description)
				.IsRequired(false)
				.HasMaxLength(4000);
			builder.Property(x => x.Country)
				.IsRequired(false)
				.HasMaxLength(80);
			builder.Property(x => x.UserLevel)
				.IsRequired(false)
				.HasMaxLength(70);
			builder.Property(x => x.Password)
				.IsRequired()
				.HasMaxLength(50);
			builder.HasMany(s => s.Skills)
				.WithOne(u => u.AppUser)
				.HasForeignKey(u => u.UserId);
		}
	}
}