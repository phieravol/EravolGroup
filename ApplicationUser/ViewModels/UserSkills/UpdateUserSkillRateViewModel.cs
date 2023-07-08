namespace Eravol.WebApi.ViewModels.UserSkills
{
    public class UpdateUserSkillRateViewModel
    {
        public int UserSkillId { get; set; }
        public Guid? UserId { get; set; }
        public int Score { get; set; }
        public bool IsVerified { get; set; }

    }
}
