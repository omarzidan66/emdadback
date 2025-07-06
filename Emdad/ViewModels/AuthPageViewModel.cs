using Emdad.Models;
using System.ComponentModel.DataAnnotations;

namespace Emdad.ViewModels
{
    public class AuthPageViewModel
    {
        public LoginModel Login { get; set; } = new LoginModel();
        public RegisterModel Register { get; set; } = new RegisterModel();
        public Citizen Citizen { get; set; }
    }

    public class LoginModel
    {
        [Required(ErrorMessage = "اسم المستخدم مطلوب")]
        public string CitizenNationalId { get; set; }

        [Required(ErrorMessage = "كلمة المرور مطلوبة")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessage = "الرقم الوطني مطلوب")]
        public string CitizenNationalId { get; set; }

        [Required(ErrorMessage = "البريد الإلكتروني مطلوب")]
        [EmailAddress(ErrorMessage = "صيغة البريد الإلكتروني غير صحيحة")]
        public string Email { get; set; }

        [Required(ErrorMessage = "كلمة المرور مطلوبة")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "تأكيد كلمة المرور مطلوب")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "كلمتا المرور غير متطابقتين")]
        public string ConfirmPassword { get; set; }
    }
}
