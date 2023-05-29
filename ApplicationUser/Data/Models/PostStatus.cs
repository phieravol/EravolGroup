namespace Eravol.WebApi.Data.Models
{
	public class PostStatus
	{
		#region Fields
		public int PostStatusId { get; set; }
		public string PostStatusName { get; set; }
		public string PostStatusDesc { get; set; }
		#endregion

		#region RelationshipFields
		public virtual ICollection<Post> Posts { get; set; }
		#endregion
	}
}
