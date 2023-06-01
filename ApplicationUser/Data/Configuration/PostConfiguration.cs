using Eravol.WebApi.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eravol.WebApi.Data.Configuration
{
	public class PostConfiguration : IEntityTypeConfiguration<Post>
	{
		public void Configure(EntityTypeBuilder<Post> builder)
		{
			#region ConfigFieldsData

			builder.ToTable("Post");
			builder.HasKey(x => x.PostId);
			builder.Property(x => x.PostTitle).IsRequired()
				.HasMaxLength(800);
			builder.Property(x => x.SortDesc).IsRequired(false)
				.HasMaxLength(2000);
			builder.Property(x => x.PostDetails).IsRequired();
			builder.Property(x => x.Budget).IsRequired();
			builder.Property(x => x.PostedDate).IsRequired();
			builder.Property(x => x.ExpirationDate).IsRequired();
			builder.Property(x => x.LevelRequired)
				.HasMaxLength(50);
			#endregion

			
		}
	}
}
