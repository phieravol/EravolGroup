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
    }
}
