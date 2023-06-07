using Eravlol.UserWebApi.Data.Models;
using Eravol.WebApi.Data.Models;

namespace Eravol.UserWebApi.Data.Models
{
	public class Skill
	{
        #region Fields
        public int Id { get; set; }
		public string SkillName { get; set; }
        public bool? isPublic { get; set; }
        #endregion

        #region Constraints
        public virtual ICollection<PostSkillRequired> PostSkillRequired { get; set; }
        public virtual ICollection<UserSkill> UserSkills { get; set; }
        #endregion
    }
}
