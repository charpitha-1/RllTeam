using System.ComponentModel.DataAnnotations;

namespace FoodappMVC.DTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Name required")]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; } // Used only in create/update
        [Required]
        [Compare("password", ErrorMessage = "Password and Confirm Password must match")]
        public string confirmPassword { get; set; }
        [Required]
        public string phoneNumber { get; set; }
        public string? userRole { get; set; }
        public string? CaptchaI { get; set; }
        [Required]
        public string? CaptchaText { get; set; } 
    }
    public class CaptchaGenerator
    {
        public static string GenerateCaptchaCode(int length = 5)
        {
            const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz23456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
