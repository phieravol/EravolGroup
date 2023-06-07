using Eravol.WebApi.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eravol.WebApi.Data.Configuration
{
    public class UserSkillConfiguration : IEntityTypeConfiguration<UserSkill>
    {
        public void Configure(EntityTypeBuilder<UserSkill> builder)
        {
            builder.ToTable("UserSkill");
            builder.HasKey(x => x.UserSkillId);

            builder.HasOne(ps => ps.AppUser)
                .WithMany(p => p.UserSkills)
                .HasForeignKey(ps => ps.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(ps => ps.Skill)
                .WithMany(s => s.UserSkills)
                .HasForeignKey(ps => ps.SkillId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
