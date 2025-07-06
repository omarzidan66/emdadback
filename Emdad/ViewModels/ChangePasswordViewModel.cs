using Emdad.Models;
using System.ComponentModel.DataAnnotations;

namespace Emdad.ViewModels
{
    public class ChangePasswordViewModel
    {
        public string CitizenId { get; set; } // The CitizenId will be passed from the logged-in user

        [Required(ErrorMessage = "Current password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "New password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        [Compare("NewPassword", ErrorMessage = "New password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }


        public List<Models.CitizensSettings> ListCitizensSettings { get; set; }


    }
}
