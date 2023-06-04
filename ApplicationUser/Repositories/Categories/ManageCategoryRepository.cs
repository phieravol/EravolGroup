using Eravol.UserWebApi.Data;
using Eravol.WebApi.Data.Models;
using Eravol.WebApi.ViewModels.Base;
using Eravol.WebApi.ViewModels.Categories;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Eravol.WebApi.Repositories.Categories
{
    public class ManageCategoryRepository : IManageCategoryRepository
    {
        private readonly EravolUserWebApiContext context;

        public ManageCategoryRepository(EravolUserWebApiContext context)
        {
            this.context = context;
        }

		public async Task CreateCategoryAsync(Category category)
		{
            try
            {
                context.Categories.Add(category);
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
		}

		public async Task<List<Category>> GetCategorySearchPaging(PagingRequestBase<Category> request)
        {
            try
            {
                IQueryable<Category> query = context.Categories
                .Include(c => c.Posts)
                .Include(c => c.Services);
                if (!string.IsNullOrEmpty(request.SearchTerm))
                {
                    query = query.Where(x => x.CategoryName.Contains(request.SearchTerm) || x.CategoryDesc.Contains(request.SearchTerm));
                }
                request.Items = await query.ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return request.Items;
        }
    }
}
