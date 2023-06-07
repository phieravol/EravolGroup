using Eravol.WebApi.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eravol.WebApi.Data.Configuration
{
    public class PostSkillRequiredConfiguration : IEntityTypeConfiguration<PostSkillRequired>
    {
        public void Configure(EntityTypeBuilder<PostSkillRequired> builder)
        {
            builder.ToTable("PostSkillRequired");
            builder.HasKey(x => x.Id);

            builder.HasOne(ps => ps.Post)
                .WithMany(p => p.PostSkillRequired)
                .HasForeignKey(ps => ps.PostId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(ps => ps.Skill)
                .WithMany(s => s.PostSkillRequired)
                .HasForeignKey(ps => ps.SkillId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
