using Eravol.WebApi.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eravol.WebApi.Data.Configuration
{
	public class CategoryConfiguration : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			#region Config Fields
			builder.ToTable("Category");
			builder.HasKey(x => x.CategoryId);
			builder.Property(x => x.CategoryName).IsRequired()
				.HasMaxLength(50);
			builder.Property(x => x.CategoryDesc).IsRequired(false)
				.HasMaxLength(500);
			builder.Property(x => x.CategoryImage).IsRequired(false);
			#endregion

			#region Config relationships
			builder.HasMany(x => x.Posts).WithOne(x => x.Categories)
				.HasForeignKey(x => x.CategoryId);
			#endregion
		}
	}
}
