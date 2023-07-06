using Eravlol.UserWebApi.Data.Models;
using Eravol.UserWebApi.Data.Configuration;
using Eravol.UserWebApi.Data.Models;
using Eravol.UserWebApi.Data.Seeding;
using Eravol.WebApi.Data.Configuration;
using Eravol.WebApi.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Eravol.UserWebApi.Data
{
    public class EravolUserWebApiContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        public EravolUserWebApiContext(DbContextOptions options) : base(options)
        {
        }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				var builder = new ConfigurationBuilder()
								 .SetBasePath(Directory.GetCurrentDirectory())
								 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
				IConfigurationRoot configuration = builder.Build();
				optionsBuilder.UseSqlServer(configuration.GetConnectionString("EravlolUserWebApiContextConnection"));
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new AppUserConfiguration());
			modelBuilder.ApplyConfiguration(new SkillConfiguration());
			modelBuilder.ApplyConfiguration(new UserImageConfiguration());
			modelBuilder.ApplyConfiguration(new CategoryConfiguration());
			modelBuilder.ApplyConfiguration(new PostConfiguration());
			modelBuilder.ApplyConfiguration(new PostStatusConfiguration());
			modelBuilder.ApplyConfiguration(new ServiceConfiguration());
			modelBuilder.ApplyConfiguration(new ServiceStatusConfiguration());
			modelBuilder.ApplyConfiguration(new ServiceImageConfiguration());
			modelBuilder.ApplyConfiguration(new PostSkillRequiredConfiguration());
			modelBuilder.ApplyConfiguration(new UserSkillConfiguration());
			modelBuilder.ApplyConfiguration(new ExperienceConfiguration());
			modelBuilder.ApplyConfiguration(new PortfolioConfiguration());
			modelBuilder.ApplyConfiguration(new CertificateConfiguration());

			base.OnModelCreating(modelBuilder);
			new DataSeeder(modelBuilder).Seed();
		}

		#region DbSet
		public DbSet<AppUser> AppUsers { get; set; }
		public DbSet<Skill> Skills { get; set; }
		public DbSet<UserImage> UserImages { get; set; }
		public DbSet<Post> Posts { get; set; }
		public DbSet<PostStatus> PostStatuses { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Service> Services { get; set; }
		public DbSet<ServiceImage> ServicesImages { get; set; }
		public DbSet<ServiceStatus> ServiceStatuses { get; set; }
		public DbSet<PostSkillRequired> PostSkilRequires { get; set; }
		public DbSet<UserSkill> UserSkills { get; set; }
		public DbSet<Experience> Experiences { get; set; }
		public DbSet<Portfolio> Portfolios { get; set; }
		public DbSet<Certificate> Certificates { get; set; }
        #endregion
    }
}
