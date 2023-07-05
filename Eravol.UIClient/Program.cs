using Eravlol.UserWebApi.Data.Models;
using Eravol.UIClient.Repositories.Categories;
using Eravol.UIClient.Repositories.General;
using Eravol.UIClient.Repositories.Posts.Clients;
using Eravol.UIClient.Repositories.Posts.Public;
using Eravol.UIClient.Repositories.Services.Freelancers;
using Eravol.UIClient.Repositories.Services.Public;
using Eravol.UIClient.Repositories.Users;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
	options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
	options.AccessDeniedPath = "/Forbidden";
});

builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddTransient<ILoginApiClient, LoginApiClient>();
builder.Services.AddTransient<IFreelancerServices, FreelancerServices>();
builder.Services.AddTransient<IClientPostsRepository, ClientPostsRepository>();
builder.Services.AddTransient<IPublicCategoryService, PublicCategoryService>();
builder.Services.AddTransient<IPostRepository, PostRepository>();
builder.Services.AddTransient<IPublicServices, PublicServices>();
builder.Services.AddTransient(typeof(IClientsRequestService<>), typeof(ClientsRequestService<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
