using Eravol.UserWebApi.Data.Models;
using Eravol.WebApi.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eravol.WebApi.Data.Configuration
{
	public class PostStatusConfiguration : IEntityTypeConfiguration<PostStatus>
	{
		public void Configure(EntityTypeBuilder<PostStatus> builder)
		{
			builder.ToTable("PostStatus");
			builder.HasKey(x => x.PostStatusId);
			builder.Property(x =>x.PostStatusName)
				.IsRequired()
				.HasMaxLength(50);
			builder.Property(x => x.PostStatusDesc)
				.IsRequired()
				.HasMaxLength(800);

			#region ConfigDataRelationships
			builder.HasMany(x => x.Posts).WithOne(x => x.PostStatus)
				.HasForeignKey(x => x.PostStatusId);
			#endregion
		}
	}
}
