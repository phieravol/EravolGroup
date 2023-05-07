using Eravol.UserWebApi.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eravol.UserWebApi.Data.Configuration
{
	public class SkillConfiguration : IEntityTypeConfiguration<Skill>
	{
		public void Configure(EntityTypeBuilder<Skill> builder)
		{
			builder.ToTable("Skill");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.SkillName)
				.IsRequired()
				.HasMaxLength(50);
			builder.Property(x => x.Score)
				.HasDefaultValue(0);
			
		}
	}
}
