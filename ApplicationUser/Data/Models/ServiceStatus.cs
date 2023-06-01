namespace Eravol.WebApi.Data.Models
{
	public class ServiceStatus
	{
		#region Fields
		public int ServiceStatusId { get; set; }
		public string ServiceStatusName { get; set; }
		public string ServiceStatusDesc { get; set; }
		#endregion

		#region Relationships
		public virtual ICollection<Service> Services { get; set; }
		#endregion
	}
}
