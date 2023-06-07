using Eravol.WebApi.Data.Models;

namespace Eravol.WebApi.ViewModels.Categories
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string? CategoryImage { get; set; }
        public bool isCategoryActive { get; set; }
        public string? CategoryDesc { get; set; }
        public int? CategoryLevel { get; set; }
        public int? CategoryParent { get; set; }
        public List<Post>? Posts { get; set; }
        public List<Service>? Services { get; set; }
    }
}
