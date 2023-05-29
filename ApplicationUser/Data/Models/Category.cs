namespace Eravol.WebApi.Data.Models
{
	public class Category
	{
		#region Fields
		public int CategoryId { get; set; }
		public string CategoryName { get; set; }
		public string? CategoryImage { get; set; }
		public bool isCategoryActive { get; set; }
		public string? CategoryDesc { get; set; }
		public int? CategoryLevel { get; set; }
		public int? CategoryParent { get; set; }
		#endregion

		#region Relationships
		public virtual ICollection<Post> Posts { get; set; }
		#endregion
	}
}
