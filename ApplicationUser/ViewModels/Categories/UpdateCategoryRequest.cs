using System.ComponentModel.DataAnnotations;

namespace Eravol.WebApi.ViewModels.Categories
{
	public class UpdateCategoryRequest
	{
		public int CategoryId { get; set; }

		[Required(ErrorMessage = "Category Name is required.")]
		public string CategoryName { get; set; }
		public IFormFile? CategoryImage { get; set; }

		[Required(ErrorMessage = "IsActive is required.")]
		public bool isCategoryActive { get; set; }
		[Required(ErrorMessage = "Category Description is required.")]
		public string? CategoryDesc { get; set; }
		public int? CategoryLevel { get; set; }
		public int? CategoryParent { get; set; }
	}
}
