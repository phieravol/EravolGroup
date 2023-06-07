﻿// <auto-generated />
using System;
using Eravol.UserWebApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Eravol.WebApi.Migrations
{
    [DbContext(typeof(EravolUserWebApiContext))]
    [Migration("20230607164510_UpdateFieldPost")]
    partial class UpdateFieldPost
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Eravlol.UserWebApi.Data.Models.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<string>("Currency")
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<string>("Description")
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTime>("MemberSince")
                        .HasColumnType("datetime2");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tagline")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserLevel")
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool?>("isAccountEnable")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("aedc1266-b3b5-4323-b10b-f020a31f3359"),
                            AccessFailedCount = 0,
                            Address = "Thai Binh",
                            ConcurrencyStamp = "28f87290-523e-42ef-a992-05ed82d02b6a",
                            Country = "VietNam",
                            Email = "eravolgroup@gmail.com",
                            EmailConfirmed = false,
                            FirstName = "Elio",
                            LastName = "Nguyen",
                            LockoutEnabled = false,
                            MemberSince = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NormalizedUserName = "ROOTADMIN",
                            Password = "Admin@123",
                            PhoneNumberConfirmed = false,
                            TwoFactorEnabled = false,
                            UserName = "RootAdmin"
                        },
                        new
                        {
                            Id = new Guid("ae750391-4d11-4e00-8e92-607d18b839cf"),
                            AccessFailedCount = 0,
                            Address = "Yen Bai",
                            ConcurrencyStamp = "a1108183-71be-4dbd-83b6-05117e9d67e0",
                            Country = "VietNam",
                            Email = "phinqevol@gmail.com",
                            EmailConfirmed = false,
                            FirstName = "Nguyen",
                            LastName = "Quoc Phi",
                            LockoutEnabled = false,
                            MemberSince = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NormalizedUserName = "PHINQ",
                            Password = "Phinq@2001",
                            PhoneNumberConfirmed = false,
                            TwoFactorEnabled = false,
                            UserName = "phinq"
                        },
                        new
                        {
                            Id = new Guid("01a033a2-ddf4-4986-8cc9-4e117f7c8685"),
                            AccessFailedCount = 0,
                            Address = "Hung Yen",
                            ConcurrencyStamp = "52430e9e-33b1-4132-9a93-f0c7321cb893",
                            Country = "VietNam",
                            Email = "chitung@gmail.com",
                            EmailConfirmed = false,
                            FirstName = "Nguyen",
                            LastName = "Chi Tung",
                            LockoutEnabled = false,
                            MemberSince = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NormalizedUserName = "TUNGNC",
                            Password = "Tungnc@9999",
                            PhoneNumberConfirmed = false,
                            TwoFactorEnabled = false,
                            UserName = "tungnc"
                        });
                });

            modelBuilder.Entity("Eravol.UserWebApi.Data.Models.Skill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("SkillName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool?>("isPublic")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Skill", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            SkillName = "C# programing"
                        },
                        new
                        {
                            Id = 2,
                            SkillName = "Bussiness Analyst"
                        },
                        new
                        {
                            Id = 3,
                            SkillName = "Web development"
                        });
                });

            modelBuilder.Entity("Eravol.UserWebApi.Data.Models.UserImage", b =>
                {
                    b.Property<int>("ImgageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ImgageId"), 1L, 1);

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserImagePath")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<long?>("UserImageSize")
                        .HasColumnType("bigint");

                    b.Property<bool>("isThumbnail")
                        .HasColumnType("bit");

                    b.Property<bool>("isUserAvatar")
                        .HasColumnType("bit");

                    b.HasKey("ImgageId");

                    b.HasIndex("UserId");

                    b.ToTable("UserImage", (string)null);
                });

            modelBuilder.Entity("Eravol.WebApi.Data.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"), 1L, 1);

                    b.Property<string>("CategoryDesc")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("CategoryImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CategoryLevel")
                        .HasColumnType("int");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("CategoryParent")
                        .HasColumnType("int");

                    b.Property<bool>("isCategoryActive")
                        .HasColumnType("bit");

                    b.HasKey("CategoryId");

                    b.ToTable("Category", (string)null);
                });

            modelBuilder.Entity("Eravol.WebApi.Data.Models.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PostId"), 1L, 1);

                    b.Property<decimal>("Budget")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastUpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LevelRequired")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PostDetails")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PostStatusId")
                        .HasColumnType("int");

                    b.Property<string>("PostTitle")
                        .IsRequired()
                        .HasMaxLength(800)
                        .HasColumnType("nvarchar(800)");

                    b.Property<DateTime>("PostedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SortDesc")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PostId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("PostStatusId");

                    b.HasIndex("UserId");

                    b.ToTable("Post", (string)null);
                });

            modelBuilder.Entity("Eravol.WebApi.Data.Models.PostSkillRequired", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<int>("SkillId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("SkillId");

                    b.ToTable("PostSkillRequired", (string)null);
                });

            modelBuilder.Entity("Eravol.WebApi.Data.Models.PostStatus", b =>
                {
                    b.Property<int>("PostStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PostStatusId"), 1L, 1);

                    b.Property<string>("PostStatusDesc")
                        .IsRequired()
                        .HasMaxLength(800)
                        .HasColumnType("nvarchar(800)");

                    b.Property<string>("PostStatusName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("PostStatusId");

                    b.ToTable("PostStatuse", (string)null);

                    b.HasData(
                        new
                        {
                            PostStatusId = 1,
                            PostStatusDesc = "When User not yet Public Post, visible by Freelancer",
                            PostStatusName = "Draft"
                        },
                        new
                        {
                            PostStatusId = 2,
                            PostStatusDesc = "When User published the Post, can be visible",
                            PostStatusName = "On Going"
                        },
                        new
                        {
                            PostStatusId = 3,
                            PostStatusDesc = "When The Post is out date, can be visible",
                            PostStatusName = "Expired"
                        },
                        new
                        {
                            PostStatusId = 4,
                            PostStatusDesc = "When The Post is Delete by Clients, Unvisible by anyone",
                            PostStatusName = "Deleted"
                        },
                        new
                        {
                            PostStatusId = 5,
                            PostStatusDesc = "When The Post is Locked by Clients, Can visible by anyone",
                            PostStatusName = "Locked"
                        });
                });

            modelBuilder.Entity("Eravol.WebApi.Data.Models.Service", b =>
                {
                    b.Property<string>("ServiceCode")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("AppUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("CategoryId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("ServiceAuthor")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ServiceDetails")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ServiceIntro")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<int>("ServiceStatusId")
                        .HasColumnType("int");

                    b.Property<string>("ServiceTitle")
                        .IsRequired()
                        .HasMaxLength(800)
                        .HasColumnType("nvarchar(800)");

                    b.Property<int?>("TotalClients")
                        .HasColumnType("int");

                    b.Property<int?>("TotalStars")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ServiceCode");

                    b.HasIndex("AppUserId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ServiceStatusId");

                    b.ToTable("Service", (string)null);
                });

            modelBuilder.Entity("Eravol.WebApi.Data.Models.ServiceImage", b =>
                {
                    b.Property<int>("ServiceImgageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ServiceImgageId"), 1L, 1);

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("ServiceCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ServiceImagePath")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<int?>("ServiceImageSize")
                        .HasColumnType("int");

                    b.Property<bool>("isThumbnail")
                        .HasColumnType("bit");

                    b.HasKey("ServiceImgageId");

                    b.HasIndex("ServiceCode");

                    b.ToTable("ServiceImage", (string)null);
                });

            modelBuilder.Entity("Eravol.WebApi.Data.Models.ServiceStatus", b =>
                {
                    b.Property<int>("ServiceStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ServiceStatusId"), 1L, 1);

                    b.Property<string>("ServiceStatusDesc")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("ServiceStatusName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ServiceStatusId");

                    b.ToTable("ServiceStatus", (string)null);

                    b.HasData(
                        new
                        {
                            ServiceStatusId = 1,
                            ServiceStatusDesc = "When the service can still be ordered by clients",
                            ServiceStatusName = "Available"
                        },
                        new
                        {
                            ServiceStatusId = 2,
                            ServiceStatusDesc = "When a freelancer is busy with multiple tasks, you can still request their services and join the queue.",
                            ServiceStatusName = "Busy"
                        },
                        new
                        {
                            ServiceStatusId = 3,
                            ServiceStatusDesc = "When the freelancer has stopped providing this service.",
                            ServiceStatusName = "Cancel"
                        });
                });

            modelBuilder.Entity("Eravol.WebApi.Data.Models.UserSkill", b =>
                {
                    b.Property<int>("UserSkillId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserSkillId"), 1L, 1);

                    b.Property<bool>("IsVerified")
                        .HasColumnType("bit");

                    b.Property<int?>("Score")
                        .HasColumnType("int");

                    b.Property<int>("SkillId")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserSkillId");

                    b.HasIndex("SkillId");

                    b.HasIndex("UserId");

                    b.ToTable("UserSkill", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("30a990c6-33c7-4884-9dcb-718ce356eb0d"),
                            ConcurrencyStamp = "7442dede-2d9d-41bc-b185-28248b7fb246",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = new Guid("b8fd818f-63f1-49ee-bec5-f7b66cafbfca"),
                            ConcurrencyStamp = "554e5776-da0a-46b2-b248-f32af8a193b6",
                            Name = "Freelancer",
                            NormalizedName = "FREELANCER"
                        },
                        new
                        {
                            Id = new Guid("fe0e9c2d-6abd-4f73-a635-63fc58ec700e"),
                            ConcurrencyStamp = "8c6cbf0d-a0f2-487a-a556-80b1ef24815b",
                            Name = "Client",
                            NormalizedName = "CLIENT"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = new Guid("aedc1266-b3b5-4323-b10b-f020a31f3359"),
                            RoleId = new Guid("30a990c6-33c7-4884-9dcb-718ce356eb0d")
                        },
                        new
                        {
                            UserId = new Guid("ae750391-4d11-4e00-8e92-607d18b839cf"),
                            RoleId = new Guid("b8fd818f-63f1-49ee-bec5-f7b66cafbfca")
                        },
                        new
                        {
                            UserId = new Guid("01a033a2-ddf4-4986-8cc9-4e117f7c8685"),
                            RoleId = new Guid("b8fd818f-63f1-49ee-bec5-f7b66cafbfca")
                        },
                        new
                        {
                            UserId = new Guid("01a033a2-ddf4-4986-8cc9-4e117f7c8685"),
                            RoleId = new Guid("fe0e9c2d-6abd-4f73-a635-63fc58ec700e")
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Eravol.UserWebApi.Data.Models.UserImage", b =>
                {
                    b.HasOne("Eravlol.UserWebApi.Data.Models.AppUser", "AppUser")
                        .WithMany("UserImages")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("Eravol.WebApi.Data.Models.Post", b =>
                {
                    b.HasOne("Eravol.WebApi.Data.Models.Category", "Categories")
                        .WithMany("Posts")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Eravol.WebApi.Data.Models.PostStatus", "PostStatus")
                        .WithMany("Posts")
                        .HasForeignKey("PostStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Eravlol.UserWebApi.Data.Models.AppUser", "AppUser")
                        .WithMany("Posts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");

                    b.Navigation("Categories");

                    b.Navigation("PostStatus");
                });

            modelBuilder.Entity("Eravol.WebApi.Data.Models.PostSkillRequired", b =>
                {
                    b.HasOne("Eravol.WebApi.Data.Models.Post", "Post")
                        .WithMany("PostSkillRequired")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Eravol.UserWebApi.Data.Models.Skill", "Skill")
                        .WithMany("PostSkillRequired")
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("Skill");
                });

            modelBuilder.Entity("Eravol.WebApi.Data.Models.Service", b =>
                {
                    b.HasOne("Eravlol.UserWebApi.Data.Models.AppUser", "AppUser")
                        .WithMany("Services")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Eravol.WebApi.Data.Models.Category", "Categories")
                        .WithMany("Services")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Eravol.WebApi.Data.Models.ServiceStatus", "ServiceStatus")
                        .WithMany("Services")
                        .HasForeignKey("ServiceStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");

                    b.Navigation("Categories");

                    b.Navigation("ServiceStatus");
                });

            modelBuilder.Entity("Eravol.WebApi.Data.Models.ServiceImage", b =>
                {
                    b.HasOne("Eravol.WebApi.Data.Models.Service", "Service")
                        .WithMany("ServiceImages")
                        .HasForeignKey("ServiceCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Service");
                });

            modelBuilder.Entity("Eravol.WebApi.Data.Models.UserSkill", b =>
                {
                    b.HasOne("Eravol.UserWebApi.Data.Models.Skill", "Skill")
                        .WithMany("UserSkills")
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Eravlol.UserWebApi.Data.Models.AppUser", "AppUser")
                        .WithMany("UserSkills")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AppUser");

                    b.Navigation("Skill");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("Eravlol.UserWebApi.Data.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("Eravlol.UserWebApi.Data.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Eravlol.UserWebApi.Data.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("Eravlol.UserWebApi.Data.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Eravlol.UserWebApi.Data.Models.AppUser", b =>
                {
                    b.Navigation("Posts");

                    b.Navigation("Services");

                    b.Navigation("UserImages");

                    b.Navigation("UserSkills");
                });

            modelBuilder.Entity("Eravol.UserWebApi.Data.Models.Skill", b =>
                {
                    b.Navigation("PostSkillRequired");

                    b.Navigation("UserSkills");
                });

            modelBuilder.Entity("Eravol.WebApi.Data.Models.Category", b =>
                {
                    b.Navigation("Posts");

                    b.Navigation("Services");
                });

            modelBuilder.Entity("Eravol.WebApi.Data.Models.Post", b =>
                {
                    b.Navigation("PostSkillRequired");
                });

            modelBuilder.Entity("Eravol.WebApi.Data.Models.PostStatus", b =>
                {
                    b.Navigation("Posts");
                });

            modelBuilder.Entity("Eravol.WebApi.Data.Models.Service", b =>
                {
                    b.Navigation("ServiceImages");
                });

            modelBuilder.Entity("Eravol.WebApi.Data.Models.ServiceStatus", b =>
                {
                    b.Navigation("Services");
                });
#pragma warning restore 612, 618
        }
    }
}
