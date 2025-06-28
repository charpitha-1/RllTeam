
using FoodDeliveryMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System.Reflection;
using System.Text;

namespace FoodDeliveryMVC.Controllers
{
    public class AccountController : Controller
    {

        [HttpGet]
        public IActionResult Registration()
        {

            string storeCaptcha = CaptchaGenerator.GenerateCaptcha(6);
            HttpContext.Session.SetString("Captcha", storeCaptcha);
            var model = new Register
            {
                CaptchaI = storeCaptcha
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Registration(Register model)
        {
            if(!ModelState.IsValid)
            {
                string storeCaptcha = CaptchaGenerator.GenerateCaptcha(6);
                model.CaptchaI = storeCaptcha;
                HttpContext.Session.SetString("Captcha", storeCaptcha);
                model.CaptchaText=string.Empty;
                return View(model);
            }
            string expectedCaptcha = HttpContext.Session.GetString("Captcha"); // from session
            string userEnteredCaptcha = model.CaptchaText?.Trim();

            if (expectedCaptcha != userEnteredCaptcha)
            {
                ModelState.AddModelError("CaptchaText", "CAPTCHA does not match.");
                string newCaptcha = CaptchaGenerator.GenerateCaptcha(6);
                HttpContext.Session.SetString("Captcha", newCaptcha);
                model.CaptchaI = newCaptcha;
                model.CaptchaText = string.Empty;
                return View(model);
            }

            //if (ModelState.IsValid)
            //{
                // Registration successful
                return RedirectToAction("Dashboard", "HomePage");
            //}

            // Re-generate CAPTCHA for retry


            //return Ok(model);
        }



        [HttpGet]
        public IActionResult Login(LoginD model)

        {
            string storeCaptcha = CaptchaGenerator.GenerateCaptcha(6);
            HttpContext.Session.SetString("Captcha", storeCaptcha);

            model.CaptchaI = storeCaptcha;
            return View(model);
        }
        [HttpPost]
        public IActionResult Index()
        {
            RedirectToAction("LogOut", "Account");
            return View();
        }

    }
}
