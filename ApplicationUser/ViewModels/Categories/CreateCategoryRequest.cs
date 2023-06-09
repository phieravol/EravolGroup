using System.ComponentModel.DataAnnotations;

namespace Eravol.WebApi.ViewModels.Categories
{
	public class CreateCategoryRequest
	{
		public string? CategoryName { get; set; }
		public IFormFile? CategoryImage { get; set; }
		public bool isCategoryActive { get; set; }
		public string? CategoryDesc { get; set; }
	}
}
