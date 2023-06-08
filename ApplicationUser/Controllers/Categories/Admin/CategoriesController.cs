using Eravol.WebApi.Data.Models;
using Eravol.WebApi.Repositories.Categories.Admin;
using Eravol.WebApi.Repositories.Images;
using Eravol.WebApi.ViewModels.Base;
using Eravol.WebApi.ViewModels.Categories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using System.Net;

namespace Eravol.WebApi.Controllers.Categories.Admin
{
    [Route("api/Admin/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IManageCategoryRepository manageCategoryService;

        public CategoriesController(
            IManageCategoryRepository manageCategoryService
        )
        {
            this.manageCategoryService = manageCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories([FromQuery] PagingRequestBase<Category> request)
        {
            request.SearchTerm = WebUtility.UrlDecode(request.SearchTerm);

            List<Category> categories = await manageCategoryService.GetCategorySearchPaging(request);

            request.TotalPages = (int)Math.Ceiling(categories.Count() / (double)request.PageSize);

            categories = categories.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize).ToList();
            request.Items = categories;
            return Ok(request);
        }

        [HttpGet("CategoryId")]
		public async Task<IActionResult> GetCategoryById(int? CategoryId)
		{
            if (CategoryId is null) return NotFound("Category Id not found");
            Category? category = await manageCategoryService.GetCategoryByIdAsync(CategoryId);

            if (category is null)
            {
                return NotFound("Category not found");
            }
			return Ok(category);
		}

		[HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequest request)
        {
            request.CategoryDesc = WebUtility.UrlDecode(request.CategoryDesc);
            request.CategoryName = WebUtility.UrlDecode(request.CategoryName);
            
            await manageCategoryService.CreateCategoryAsync(request);
			return Created("./Index", request);
        }
        [HttpPut("{CategoryId}")]
		public async Task<IActionResult> UpdateMember(int? CategoryId, Category? category)
		{
			if (CategoryId is null) return NotFound("Category Id not found");
			if (CategoryId is null) return NotFound("Category is Empty");

			manageCategoryService.UpdateCategoryById(category);

			return NoContent();
		}

        [HttpDelete("{CategoryId}")]
        public async Task<IActionResult> DeleteMember(int? CategoryId)
        {
            if (CategoryId is null) return NotFound("Category Id not found");

            Category? category = await manageCategoryService.GetCategoryByIdAsync(CategoryId);

            if (category is null)
            {
                return NotFound("Category not found");
            }
            await manageCategoryService.DeleteCategoryAsync(category);

            return NoContent();
        }
    }
}
