using FoodappMVC.DTO;
using FoodieApp.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Net.Http;

namespace FoodappMVC.Controllers
{
    public class AccountController : Controller
    {

        private readonly IHttpClientFactory httpClientFactory;

        public AccountController(IHttpClientFactory _httpClientFactory)
        {
            httpClientFactory = _httpClientFactory;
        }


        [HttpGet]
        public IActionResult AccountLogin()
        {
            string code = CaptchaGenerator.GenerateCaptchaCode(5);
            HttpContext.Session.SetString("Captchacode", code);
            var model = new LoginDTO
            {
                CaptchaI = code,
                CaptchaText= string.Empty
            };
            ModelState.Clear();
            return View(model);
        }
    

    [HttpPost]
        public IActionResult AccountLogin(LoginDTO model)
        {
            if (!ModelState.IsValid)
            {
                string code = CaptchaGenerator.GenerateCaptchaCode(5);
                model.CaptchaI = code;//CaptchaGenerator.GenerateCaptchaCode(5);
                HttpContext.Session.SetString("Captchacode", code);
                model.CaptchaText = string.Empty;
                return View(model);

            }
            string OriginalCaptcha = HttpContext.Session.GetString("Captchacode");// gets the already stored captcha in
                                                                                  // the session to the correctcaptcha
            if (model.CaptchaText != OriginalCaptcha)
            {
                ModelState.AddModelError("CaptchaText", "Incorrect Captcha");//adding new error message if captcha doesnt validate 
                string newcaptcha = CaptchaGenerator.GenerateCaptchaCode(5);
                HttpContext.Session.SetString("Captchacode", newcaptcha);//stores the new captcha in the session as key
                                                                         //value pairs captchacode is the key
                model.CaptchaI = newcaptcha;//captchainput propert in the model gets the newcaptcha code
                model.CaptchaText = string.Empty;

                return View(model);
            }

            return RedirectToAction("Registrarion", "Order");


        }

        [HttpGet]
        public IActionResult Registration()
        {
            string captcha = CaptchaGenerator.GenerateCaptchaCode(5);
            HttpContext.Session.SetString("CaptchaCode", captcha);

            var model = new RegisterDTO
            {
                CaptchaI = captcha,
                CaptchaText = string.Empty
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegisterDTO model)
        {
            if (!ModelState.IsValid)
            {
                // Regenerate Captcha
                string newCaptcha = CaptchaGenerator.GenerateCaptchaCode(5);
                HttpContext.Session.SetString("CaptchaCode", newCaptcha);
                model.CaptchaI = newCaptcha;
                model.CaptchaText = string.Empty;

                return View(model);
            }

            string correctCaptcha = HttpContext.Session.GetString("CaptchaCode");

            if (model.CaptchaText != correctCaptcha)
            {
                ModelState.AddModelError("CaptchaText", "Incorrect Captcha");

                // Refresh Captcha again
                string newCaptcha = CaptchaGenerator.GenerateCaptchaCode(5);
                HttpContext.Session.SetString("CaptchaCode", newCaptcha);
                model.CaptchaI = newCaptcha;
                model.CaptchaText = string.Empty;

                return View(model);
            }

            // Send to Web API using HttpClient
            var client = httpClientFactory.CreateClient();
            string apiUrl = "https://localhost:7059/api/User"; // change as per your actual Web API base URL

            var userDTO = new UserDTO
            {
                firstName = model.firstName,
                lastName = model.lastName,
                email = model.email,
                password = model.password,
                phoneNumber = model.phoneNumber,
                userRole = model.userRole ?? "Customer"
            };

            var response = await client.PostAsJsonAsync(apiUrl, userDTO);
            

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Registration successful!";
                return RedirectToAction("AccountLogin", "Account");
            }
            else
            {
                string error = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"Registration failed: {error}");

                // Refresh captcha for retry
                string newCaptcha = CaptchaGenerator.GenerateCaptchaCode(5);
                HttpContext.Session.SetString("CaptchaCode", newCaptcha);
                model.CaptchaI = newCaptcha;
                model.CaptchaText = string.Empty;

                return View(model);
            }
        }

    }
}
