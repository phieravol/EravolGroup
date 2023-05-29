using Eravlol.UserWebApi.Data.Models;

namespace Eravol.WebApi.Data.Models
{
	public class Post
	{
		#region Fields
		public int PostId { get; set; }
		public string PostTitle { get; set; }
		public string? SortDesc { get; set; }
		public string PostDetails { get; set; }
		public decimal Budget { get; set; }
		public DateTime ExpirationDate { get; set; }
		public DateTime PostedDate { get; set; }
		public int CategoryId { get; set; }
		public Guid UserId { get; set; }
		public int PostStatusId { get; set; }
		public string LevelRequired { get; set; }
		#endregion Fields

		#region Constraints
		public virtual AppUser AppUser { get; set; }
		public virtual PostStatus PostStatus { get; set; }
		#endregion
	}
}
