using Eravlol.UserWebApi.Data.Models;
using Eravol.UserWebApi.Data;
using Eravol.UserWebApi.Repository.Skills;
using Eravol.UserWebApi.Repository.User.Admin;
using Eravol.WebApi.Repositories.Categories.Admin;
using Eravol.WebApi.Repositories.Categories.Public;
using Eravol.WebApi.Repositories.Images;
using Eravol.WebApi.Repositories.Posts.Clients;
using Eravol.WebApi.Repositories.Posts.Public;
using Eravol.WebApi.Repositories.PostSkills;
using Eravol.WebApi.Repositories.Services.Freelancers;
using Eravol.WebApi.Repositories.Servicestatuses.Freelancers;
using Eravol.WebApi.Repository.System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Cryptography.Xml;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;
using Eravol.WebApi.Repositories.ServiceImages.Freelancers;
using Eravol.WebApi.Repositories.Services.Publics;
using Eravol.WebApi.Repositories.UserImages;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(o => o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
	config.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger EravolGroup Solution", Version = "v1" });
	config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Description = "@JWT Authorization header using the Bearer schema. \r\n\r\n" +
			"Enter 'Bearer' [Space] and then your token in the text input below. \r\n\r\n" +
			"Example: 123456abcdef",
		Name = "Authorization",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer"
	});
	config.AddSecurityRequirement(new OpenApiSecurityRequirement()
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				},
				Scheme = "oauth2",
				Name = "Bearer",
				In = ParameterLocation.Header,
			},
			new List<string>()
		}
	});
});

// Register and Config Identity
builder.Services.AddIdentity<AppUser, IdentityRole<Guid>>()
	.AddEntityFrameworkStores<EravolUserWebApiContext>()
	.AddDefaultTokenProviders()
	.AddRoles<IdentityRole<Guid>>();

// Register and config Authentication
builder.Services.AddAuthentication(option =>
{
	option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Register and config JwtBear
.AddJwtBearer(option =>
{
	option.SaveToken = true;
	option.RequireHttpsMetadata = false;

	option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidAudience = builder.Configuration["JWT:ValidAudience"],
		ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"])),
		ValidateLifetime = true,
		ClockSkew = TimeSpan.FromDays(5)
	};


});

//Regist DbContext Service
builder.Services.AddDbContext<EravolUserWebApiContext>(options => options.UseSqlServer(
	builder.Configuration.GetConnectionString("EravlolUserWebApiContextConnection")
	));

//Add Cors service
builder.Services.AddCors(p => p.AddPolicy("corseravol", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

/**
 * Add application services
 */
builder.Services.AddTransient<UserManager<AppUser>, UserManager<AppUser>>();
builder.Services.AddTransient<SignInManager<AppUser>, SignInManager<AppUser>>();
builder.Services.AddTransient<RoleManager<IdentityRole<Guid>>, RoleManager<IdentityRole<Guid>>>();

builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<IManageProfileRepository, ManageProfileRepository>();
builder.Services.AddTransient<ISkillRepository, SkillRepository>();
builder.Services.AddTransient<IManageCategoryRepository, ManageCategoryRepository>();
builder.Services.AddTransient<IFileStorageService, FileStorageService>();
builder.Services.AddTransient<ISkillRepository, SkillRepository>();
builder.Services.AddTransient<IPublicCategoryRepository, PublicCategoryRepository>();
builder.Services.AddTransient<IClientsPostRepository, ClientsPostRepository>();
builder.Services.AddTransient<IPostSkillsRepository, PostSkillsRepository>();
builder.Services.AddTransient<IPostsPublicRepository, PostsPublicRepository>();
builder.Services.AddTransient<IManageServicesRepository, ManageServicesRepository>();
builder.Services.AddTransient<IServiceStatusesRepository, ServiceStatusesRepository>();
builder.Services.AddTransient<IServiceImagesRepository, ServiceImagesRepository>();
builder.Services.AddTransient<IServicesPublicRepository, ServicesPublicRepository>();
builder.Services.AddTransient<IUserImageRepository, UserImageRepository>();

var app = builder.Build();

var env = app.Environment;
env.ContentRootPath = Directory.GetCurrentDirectory();
env.WebRootPath = Directory.GetCurrentDirectory();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("corseravol");
app.UseAuthentication(); ;

app.UseAuthorization();

app.MapControllers();

app.Run();