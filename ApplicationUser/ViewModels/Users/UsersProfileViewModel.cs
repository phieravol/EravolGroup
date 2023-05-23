namespace Eravol.UserWebApi.ViewModels.Users
{
    public class UsersProfileViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string? Tagline { get; set; }
        public string? Description { get; set; }
        public string Country { get; set; }
        public string Currency { get; set; }
        public string? Address { get; set; }
        public DateTime MemberSince { get; set; }
        public DateTime? Birthday { get; set; }
        public string UserLevel { get; set; }
        public bool? isAccountEnable { get; set; }
        public IFormFile UserAvatar { get; set; }
    }
}
