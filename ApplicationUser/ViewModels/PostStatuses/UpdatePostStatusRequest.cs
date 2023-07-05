using System.ComponentModel.DataAnnotations;

namespace Eravol.WebApi.ViewModels.PostStatuses
{
    public class UpdatePostStatusRequest
    {
        [Required(ErrorMessage = "Post Status Name is required.")]
        public string PostStatusName { get; set; }
        public string? PostStatusDesc { get; set; }
    }
}
