using System.ComponentModel.DataAnnotations;

namespace Emdad.Models
{
    public class Citizen : BaseEntity
    {
        public int CitizenId { get; set; }
        [Required]
        public int CitizenNationalId { get; set; }
        [Required]
        [EmailAddress]
        public string CitizenEmail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string CitizenPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password Not Matche")]
        public string CitizenConfirmPassword { get; set; }
    }
}
