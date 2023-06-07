using Eravol.UserWebApi.Data.Models;

namespace Eravol.WebApi.Data.Models
{
    public class PostSkillRequired
    {
        #region Fields
        public int Id { get; set; }
        public int PostId { get; set; }
        public int SkillId { get; set; }
        #endregion

        #region Constraints
        public virtual Skill? Skill { get; set; }
        public virtual Post? Post { get; set; }
        #endregion
    }
}
