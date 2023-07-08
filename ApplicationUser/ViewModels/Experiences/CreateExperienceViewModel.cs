namespace Eravol.WebApi.ViewModels.Experiences
{
    public class CreateExperienceViewModel
    {
        public string CompanyTitle { get; set; }
        public string Position { get; set; }
        public string? JobDescription { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime? EndingDate { get; set; }
    }
}
