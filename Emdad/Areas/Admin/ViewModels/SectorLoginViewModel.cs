using System.ComponentModel.DataAnnotations;

namespace Emdad.Areas.Admin.ViewModels
{
    public class SectorLoginViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        public int SectorId { get; set; }
        public string SectorName { get; set; }
    }
}
