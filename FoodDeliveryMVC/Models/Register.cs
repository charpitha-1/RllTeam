using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FoodDeliveryMVC.Models
{
    public class Register
    {
        [Required(ErrorMessage ="Name required")]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; } // Used only in create/update
        [Required]
        public string confirmPassword {  get; set; }
        [Required]
        public string phoneNumber { get; set; }
        public string userRole { get; set; }
        public string? CaptchaI {  get; set; } = string.Empty;
        [Required]
        public string CaptchaText { get; set; }
        
    }
    public static class CaptchaGenerator{
        public static string GenerateCaptcha(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder captcha = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(chars.Length);
                captcha.Append(chars[index]);
            }

            return captcha.ToString();
        }
    }

}
