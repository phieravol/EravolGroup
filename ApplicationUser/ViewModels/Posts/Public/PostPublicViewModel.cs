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
		public int CategoryId { get; set; }
		public Guid UserId { get; set; }
		public int PostStatusId { get; set; }
		public string LevelRequired { get; set; }
	}
}
