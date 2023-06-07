using Eravlol.UserWebApi.Data.Models;
using Eravol.UserWebApi.Data.Models;

namespace Eravol.WebApi.Data.Models
{
    public class UserSkill
    {
        #region Fields
        public int UserSkillId { get; set; }
        public int SkillId { get; set; }
        public Guid UserId { get; set; }
        public int? Score { get; set; }
        public bool IsVerified { get; set; }
        #endregion

        #region Constraint
        public virtual AppUser? AppUser { get; set; }
        public virtual Skill? Skill { get; set; }
        #endregion
    }
}
