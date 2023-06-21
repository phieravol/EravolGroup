using Eravol.UserWebApi.Data;
using Eravol.WebApi.Data.Models;

namespace Eravol.WebApi.Repositories.Categories.Public
{
    public class PublicCategoryRepository : IPublicCategoryRepository
    {
        private readonly EravolUserWebApiContext context;

        public PublicCategoryRepository(EravolUserWebApiContext context)
        {
            this.context = context;
        }

        public List<Category> GetAllCategories()
        {
            return context.Categories.Where(x=>x.isCategoryActive).ToList();
        }

		public List<Category> GetCategoriesBySearchTerm(string? keyword)
		{
			List<Category> categories = new List<Category>();
			try
			{
				var query = context.Categories.Where(x => x.isCategoryActive);
				if (!string.IsNullOrEmpty(keyword))
				{
					query = query.Where(x => x.CategoryName.Contains(keyword));
				}

				categories = query.ToList();
				return categories;
			}
			catch (Exception e)
			{

				throw new Exception(e.Message);
			}
		}
	}
}
