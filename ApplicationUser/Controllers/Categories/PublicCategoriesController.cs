using Eravol.WebApi.Data.Models;
using Eravol.WebApi.Repositories.Categories.Admin;
using Eravol.WebApi.Repositories.Categories.Public;
using Eravol.WebApi.ViewModels.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Eravol.WebApi.Controllers.Categories
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicCategoriesController : ControllerBase
    {
        private readonly IPublicCategoryRepository publicCategoryService;

        public PublicCategoriesController(IPublicCategoryRepository publicCategoryService)
        {
            this.publicCategoryService = publicCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            List<Category> Categories = publicCategoryService.GetAllCategories();
            return Ok(Categories);
        }
    }
}
