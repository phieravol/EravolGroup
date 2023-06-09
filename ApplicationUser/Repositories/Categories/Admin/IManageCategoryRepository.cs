﻿using Eravol.WebApi.Data.Models;
using Eravol.WebApi.ViewModels.Base;
using Eravol.WebApi.ViewModels.Categories;

namespace Eravol.WebApi.Repositories.Categories.Admin
{
    public interface IManageCategoryRepository
    {
        Task CreateCategoryAsync(CreateCategoryRequest category);
        Task DeleteCategoryAsync(Category category);
        Task<Category> GetCategoryByIdAsync(int? categoryId);
        Task<List<Category>> GetCategorySearchPaging(PagingRequestBase<Category> request);
        Task UpdateCategoryById(UpdateCategoryRequest? category);
    }
}
