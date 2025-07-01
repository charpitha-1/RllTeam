using System.ComponentModel.DataAnnotations;

namespace FoodappMVC.DTO
{
    public class LoginRequestDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        [Required(ErrorMessage = "Captcha is required")]
        public string CaptchaText { get; set; }

        public string? CaptchaI { get; set; }
    }
}
