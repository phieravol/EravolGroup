using Eravlol.UserWebApi.Data.Models;
using Eravol.UserWebApi.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Eravol.UserWebApi.Data.Seeding
{
	public class DataSeeder
	{
		private readonly ModelBuilder modelBuilder;

		public DataSeeder(ModelBuilder modelBuilder)
		{
			this.modelBuilder = modelBuilder;
		}
		public void Seed()
		{
			// Seed role data
			modelBuilder.Entity<IdentityRole<Guid>>().HasData(
				new IdentityRole<Guid>() { Id = new Guid("30A990C6-33C7-4884-9DCB-718CE356EB0D"), Name= "Admin" },
				new IdentityRole<Guid>() { Id = new Guid("B8FD818F-63F1-49EE-BEC5-F7B66CAFBFCA"), Name= "Freelancer" },
				new IdentityRole<Guid>() { Id = new Guid("FE0E9C2D-6ABD-4F73-A635-63FC58EC700E"), Name= "Client" }
			);

			// Seed User data
			modelBuilder.Entity<AppUser>().HasData(
				new AppUser() { Id = new Guid("AEDC1266-B3B5-4323-B10B-F020A31F3359"), UserName = "RootAdmin", Password="Admin@123" ,Email = "eravolgroup@gmail.com", FirstName = "Elio", LastName = "Nguyen", Address="Thai Binh", Country = "VietNam"},
				new AppUser() { Id = new Guid("AE750391-4D11-4E00-8E92-607D18B839CF"), UserName = "phinq", Password = "Phinq@2001", Email = "phinqevol@gmail.com", FirstName = "Nguyen", LastName = "Quoc Phi", Address = "Yen Bai", Country = "VietNam" },
				new AppUser() { Id = new Guid("01A033A2-DDF4-4986-8CC9-4E117F7C8685"), UserName = "tungnc", Password = "Tungnc@9999", Email = "chitung@gmail.com", FirstName = "Nguyen", LastName = "Chi Tung", Address = "Hung Yen", Country = "VietNam" }
				);

			// Seed User-Role
			modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(
				new IdentityUserRole<Guid>() { RoleId = new Guid("30A990C6-33C7-4884-9DCB-718CE356EB0D"), UserId= new Guid("AEDC1266-B3B5-4323-B10B-F020A31F3359") },
				new IdentityUserRole<Guid>() { RoleId = new Guid("B8FD818F-63F1-49EE-BEC5-F7B66CAFBFCA"), UserId= new Guid("AE750391-4D11-4E00-8E92-607D18B839CF") },
				new IdentityUserRole<Guid>() { RoleId = new Guid("B8FD818F-63F1-49EE-BEC5-F7B66CAFBFCA"), UserId= new Guid("01A033A2-DDF4-4986-8CC9-4E117F7C8685") },
				new IdentityUserRole<Guid>() { RoleId = new Guid("FE0E9C2D-6ABD-4F73-A635-63FC58EC700E"), UserId= new Guid("01A033A2-DDF4-4986-8CC9-4E117F7C8685") }
				);

			// Seed Skill
			modelBuilder.Entity<Skill>().HasData(
				new Skill() { Id=1, SkillName = "C# programing", IsVerified = false, Score=0, UserId = new Guid("AE750391-4D11-4E00-8E92-607D18B839CF") },
                new Skill() { Id = 2, SkillName = "Bussiness Analyst", IsVerified = false, Score = 0, UserId = new Guid("AE750391-4D11-4E00-8E92-607D18B839CF") },
                new Skill() { Id = 3, SkillName = "Web development", IsVerified = false, Score = 0, UserId = new Guid("AE750391-4D11-4E00-8E92-607D18B839CF") }

                );
		}
	}
}
