using System.ComponentModel.DataAnnotations;

namespace Eravol.WebApi.Data.Models
{
	public class ServiceImage
	{
		#region
		[Key]
		public int ServiceImgageId { get; set; }
		public string ServiceImagePath { get; set; }
		public bool isThumbnail { get; set; }
		public DateTime? DateCreated { get; set; }
		public int? ServiceImageSize { get; set; }
		#endregion


		#region ReferenceKey
		public string ServiceCode { get; set; }
		#endregion


		#region relationships
		public virtual Service Service { get; set; }
		#endregion
	}
}
