using Microsoft.EntityFrameworkCore;

namespace Emdad.Models
{
    public class PasswordService
    {
        private readonly EmdadContext _context;

        public PasswordService(EmdadContext context)
        {
            _context = context;
        }

        //public bool ChangePassword(string citizenId, string currentPassword, string newPassword, string confirmPassword)
        //{
        //    var citizen = _context.Citizen
        //                              .FirstOrDefault(c => c.CitizenNationalId == citizenId);

        //    if (citizen == null)
        //    {
        //        return false; // Citizen not found
        //    }

        //    if (citizen.CitizenPassword != currentPassword)
        //    {
        //        return false; // Incorrect current password
        //    }

        //    if (newPassword != confirmPassword)
        //    {
        //        return false; // New password does not match confirm password
        //    }

        //    // Update the password logic here, then save changes
        //    citizen.CitizenPassword = newPassword;
        //    _context.SaveChanges();

        //    return true;
        //}


        private string HashPassword(string password)
        {
            // Example hashing function, replace with a secure method like BCrypt
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
}
