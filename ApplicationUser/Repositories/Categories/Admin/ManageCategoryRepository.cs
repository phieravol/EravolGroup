using Eravol.UserWebApi.Data;
using Eravol.WebApi.Data.Models;
using Eravol.WebApi.Repositories.Images;
using Eravol.WebApi.ViewModels.Base;
using Eravol.WebApi.ViewModels.Categories;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Net.Http.Headers;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Eravol.WebApi.Repositories.Categories.Admin
{
    public class ManageCategoryRepository : IManageCategoryRepository
    {
        private readonly EravolUserWebApiContext context;
        private readonly IFileStorageService storageService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";

        public ManageCategoryRepository(
            EravolUserWebApiContext context,
            IFileStorageService storageService
        )
        {
            this.context = context;
            this.storageService = storageService;
        }


        public async Task CreateCategoryAsync(CreateCategoryRequest request)
        {
            try
            {
                Category category = new Category()
                {
                    CategoryName = request.CategoryName,
                    CategoryDesc = request.CategoryDesc,
                    CategoryLevel = request.CategoryLevel,
                    CategoryParent = request.CategoryParent,
                    isCategoryActive = request.isCategoryActive
                };

                if (request.CategoryImage != null)
                {
                    category.CategoryImage = await SaveFile(request.CategoryImage);
                }

                context.Categories.Add(category);
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteCategoryAsync(Category category)
        {
            try
            {
                context.Categories.Remove(category);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Category>? GetCategoryByIdAsync(int? categoryId)
        {
            try
            {
                Category? category = await context.Categories.FindAsync(categoryId);
                return category;
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

        public void UpdateCategoryById(Category? category)
        {
            try
            {
                context.Entry<Category>(category).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
    }
}
