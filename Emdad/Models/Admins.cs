
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Emdad.Models
{
    public class Admins
    {
        [Key]
        public int AdminId { get; set; }

        // Link to ASP.NET Identity
        [Required]
        public string IdentityUserId { get; set; }

        // Personal Info
        [Required]
        public string FullName { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        // Access Control
        public int? AssignedSectorId { get; set; }

        public bool IsActive { get; set; } = true;
        public bool IsStopped { get; set; } = false;
        public DateTime? LastActiveAt { get; set; }

        // Navigation
        [ForeignKey("AssignedSectorId")]
        public Sectors Sector { get; set; }
    }
}
