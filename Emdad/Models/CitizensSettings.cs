namespace Emdad.Models
{
    public class CitizensSettings : BaseEntity
    {
        public int CitizensSettingsId { get; set; }
        public string CitizenNationalId { get; set; }
        public string? CitizenFullName { get; set; }
        public string? CitizenEmail { get; set; }
        public string? CitizenPhone { get; set; }
        public string? CitizenLocation { get; set; }
        public string? CitizenAbout { get; set; }
        public Citizen Citizen { get; set; }
    }
}
