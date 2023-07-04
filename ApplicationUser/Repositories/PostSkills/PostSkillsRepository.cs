using Eravol.UserWebApi.Data;
using Eravol.WebApi.Data.Models;
using Eravol.WebApi.ViewModels.PostSkillRequires;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Eravol.WebApi.Repositories.PostSkills
{
    public class PostSkillsRepository : IPostSkillsRepository
    {
		#region DbContext Dependency injection
		private readonly EravolUserWebApiContext context;
		#endregion

		#region Constructor
		public PostSkillsRepository(EravolUserWebApiContext context)
        {
            this.context = context;
        }
		#endregion

		/// <summary>
		/// Create Multiple SkillRequires in Db by List of PostSkillRequired
		/// </summary>
		/// <param name="skillRequires"></param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		public async Task CreateSkillsRequireAsync(List<PostSkillRequired> skillRequires)
        {
            try
            {
                context.PostSkilRequires.AddRangeAsync(skillRequires);
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            };
        }

        public async Task CreateSpecifySkillRequireAsync(PostSkillRequired skillRequired)
        {
            try
            {
                context.PostSkilRequires.AddAsync(skillRequired);
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            };
        }


        /// <summary>
        /// Delete PostSkillRequire by Id
        /// </summary>
        /// <param name="skillRequireId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task DeleteSkillRequire(PostSkillRequired skillRequired)
        {
            try
            {
                context.PostSkilRequires.Remove(skillRequired);
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            };
        }

        /// <summary>
        /// Get PostSkillRequire from DB by Id
        /// </summary>
        /// <param name="skillRequireId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<PostSkillRequired> GetSkillRequireById(int? skillRequireId)
        {
            try
            {
                PostSkillRequired? postSkill = await context.PostSkilRequires.FindAsync(skillRequireId);
                return postSkill;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            };
        }

        /// <summary>
        /// Get Skill required list by post id
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<PostSkillRequireViewModel>> GetSkillRequireByPostId(int? postId)
        {
            try
            {
                var query = from require in context.PostSkilRequires
                            join skill in context.Skills on require.SkillId equals skill.Id
                            where require.PostId == postId
                            select new { require, skill };

                List<PostSkillRequireViewModel> postSkills = await query.Select(x => new PostSkillRequireViewModel()
                {
                    PostId = x.require.PostId,
                    SkillId= x.require.SkillId,
                    SkillName = x.skill.SkillName
                }).ToListAsync();

                return postSkills;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Get skill require by search term
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
		public List<PostSkillRequireViewModel>? GetSkillRequireBySearchTerm(string? searchTerm)
		{
			try
			{
                var query = from skill in context.Skills
                            select skill;

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    query = query.Where(x => x.SkillName.Contains(searchTerm));
                }

                List<PostSkillRequireViewModel>? skills = query.Select(x => new PostSkillRequireViewModel()
                {
                    SkillId = x.Id,
                    SkillName = x.SkillName,
                }).ToList();

                return skills;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			};

		}

		/// <summary>
		/// Get skill require by skillId and postId
		/// </summary>
		/// <param name="skillRequireId"></param>
		/// <param name="postId"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<PostSkillRequired?> GetSpecificSkillRequire(int? skillRequireId, int? postId)
        {
            try
            {
                PostSkillRequired? skillRequired = await context.PostSkilRequires.FirstOrDefaultAsync(x => x.SkillId== skillRequireId && x.PostId==postId);
                return skillRequired;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            };
        }

        /// <summary>
        /// UpdateServiceRequest PostSkillRequire by current object
        /// </summary>
        /// <param name="skillRequire"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task UpdateSkillRequire(PostSkillRequired skillRequire)
        {
            try
            {

                context.Entry<PostSkillRequired>(skillRequire).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            };
        }
    }
}
