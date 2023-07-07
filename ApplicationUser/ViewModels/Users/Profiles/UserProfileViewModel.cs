namespace Eravol.WebApi.ViewModels.Users.Profiles
{
	public class UserProfileViewModel
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string? Tagline { get; set; }
		public string? Description { get; set; }
		public string? PhoneNumber { get; set; }
		public string Country { get; set; }
		public string? Address { get; set; }
		public DateTime? Birthday { get; set; }
	}
}
