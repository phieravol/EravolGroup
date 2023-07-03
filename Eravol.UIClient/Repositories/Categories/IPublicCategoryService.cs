using Eravol.WebApi.Data.Models;

namespace Eravol.UIClient.Repositories.Categories
{
    public interface IPublicCategoryService
    {
        Task<List<Category>> GetAllPublicCategory();
    }
}
