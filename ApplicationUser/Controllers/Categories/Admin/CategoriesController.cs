using Eravol.WebApi.Data.Models;
using Eravol.WebApi.Repositories.Categories;
using Eravol.WebApi.ViewModels.Base;
using Eravol.WebApi.ViewModels.Categories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Eravol.WebApi.Controllers.Categories.Admin
{
    [Route("api/Admin/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IManageCategoryRepository manageCategoryService;

        public CategoriesController(IManageCategoryRepository manageCategoryService)
        {
            this.manageCategoryService = manageCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] PagingRequestBase<Category> request)
        {
            request.SearchTerm = WebUtility.UrlDecode(request.SearchTerm);

            List<Category> categories = await manageCategoryService.GetCategorySearchPaging(request);

            request.TotalPages = (int)Math.Ceiling(categories.Count() / (double)request.PageSize);

            categories = categories.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize).ToList();
            request.Items = categories;
            return Ok(request);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequest request)
        {
            request.CategoryDesc = WebUtility.UrlDecode(request.CategoryDesc);
            request.CategoryName = WebUtility.UrlDecode(request.CategoryName);

            Category category = new Category()
            {
                CategoryName = request.CategoryName,
                CategoryDesc = request.CategoryDesc,
                CategoryLevel = 1,
                isCategoryActive = request.isCategoryActive
                
            };

            await manageCategoryService.CreateCategoryAsync(category);
			return Created("./Index", request);
        }
    }
}
