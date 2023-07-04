using Eravol.UserWebApi.Data.Models;
using Eravol.WebApi.ViewModels.PostSkillRequires;

namespace Eravol.WebApi.ViewModels.Posts.Public
{
	public class PostPublicViewModel
	{
		public int PostId { get; set; }
		public string PostTitle { get; set; }
		public string? SortDesc { get; set; }
		public string PostDetails { get; set; }
		public decimal Budget { get; set; }
		public DateTime ExpirationDate { get; set; }
		public DateTime PostedDate { get; set; }
		public DateTime? LastUpdatedDate { get; set; }
		public int? CategoryId { get; set; }
		public string? CategoryName { get; set; }
		public Guid UserId { get; set; }
		public string? FullName { get; set; }
		public string? Username { get; set; }
		public string? Country { get; set; }
		public int? PostStatusId { get; set; }
		public string? LevelRequired { get; set; }
		public List<PostSkillRequireViewModel>? SkillRequire { get; set; }
	}
}
