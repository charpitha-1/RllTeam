using System.ComponentModel.DataAnnotations;

namespace FoodappMVC.DTO
{
    public class LoginDTO
    {
        public string email { get; set; }
        public string password { get; set; }
        [Required(ErrorMessage = "Captcha is required")]
        public string CaptchaText { get; set; }

        public string? CaptchaI { get; set; }

        //[Range(typeof(bool), "true", "true", ErrorMessage = "Please confirm you are not a robot")]
        public bool NotRobot { get; set; }
    }
}
