using System.ComponentModel.DataAnnotations;

namespace Eravol.UserWebApi.ViewModels.Skills
{
    public class SkillViewModel
    {
        [Required(ErrorMessage = "Skill Name is required.")]
        public string SkillName { get; set; }
        public bool? isPublic { get; set; }
    }
}
