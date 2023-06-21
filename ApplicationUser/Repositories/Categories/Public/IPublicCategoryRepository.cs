using Eravol.WebApi.Data.Models;

namespace Eravol.WebApi.Repositories.Categories.Public
{
    public interface IPublicCategoryRepository
    {
        List<Category> GetAllCategories();
		List<Category> GetCategoriesBySearchTerm(string? keyword);
	}
}
