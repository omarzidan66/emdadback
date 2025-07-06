using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Emdad.Areas.Admin.ViewModels
{
    public class CreateAdminViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required, Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public int AssignedSectorId { get; set; }

        public List<SelectListItem> AvailableSectors { get; set; }
    }
}
