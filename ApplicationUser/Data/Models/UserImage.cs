using Eravlol.UserWebApi.Data.Models;

namespace Eravol.UserWebApi.Data.Models
{
	public class UserImage
	{
		public int ImgageId { get; set; }
		public Guid UserId { get; set; }
		public string UserImagePath { get; set;}
		public bool isUserAvatar { get; set; }
		public bool isThumbnail { get; set; }
		public DateTime? DateCreated { get; set; }
		public int? UserImageSize { get; set; }
		public virtual AppUser? AppUser { get; set; }
	}
}
