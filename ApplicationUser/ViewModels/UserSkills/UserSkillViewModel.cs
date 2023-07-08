namespace Eravol.WebApi.ViewModels.UserSkills
{
    public class UserSkillViewModel
    {
        public int UserSkillId { get; set; }
        public string UserSkillName { get; set; }
        public int SkillId { get; set; }
        public Guid UserId { get; set; }
        public int? Score { get; set; }
        public bool IsVerified { get; set; }
    }
}
