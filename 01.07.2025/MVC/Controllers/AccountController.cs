using FoodappMVC.DTO;
using FoodieApp.DTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;

namespace FoodappMVC.Controllers
{
    public class AccountController : Controller
    {

        private readonly IHttpClientFactory httpClientFactory;

        public AccountController(IHttpClientFactory _httpClientFactory)
        {
            httpClientFactory = _httpClientFactory;
        }


        [Authorize]
        public IActionResult UserDashboard()
        {
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";
            return View();
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
        
        public async Task<IActionResult> AccountLogin(LoginRequestDTO model)
        {
            if (!ModelState.IsValid)
            {
                // Regenerate captcha
                string code = CaptchaGenerator.GenerateCaptchaCode(5);
                model.CaptchaI = code;
                HttpContext.Session.SetString("Captchacode", code);
                model.CaptchaText = string.Empty;
                return View(model);
            }

            string originalCaptcha = HttpContext.Session.GetString("Captchacode");
            if (model.CaptchaText != originalCaptcha)
            {
                ModelState.AddModelError("CaptchaText", "Incorrect Captcha");
                string newCaptcha = CaptchaGenerator.GenerateCaptchaCode(5);
                HttpContext.Session.SetString("Captchacode", newCaptcha);
                model.CaptchaI = newCaptcha;
                model.CaptchaText = string.Empty;
                return View(model);
            }

            // Call Web API to validate credentials
            var client = httpClientFactory.CreateClient();
            var apiUrl = "https://localhost:7059/api/User/Login";

            var response = await client.PostAsJsonAsync(apiUrl, model);

            if (response.IsSuccessStatusCode)
            {
                var loginData = await response.Content.ReadFromJsonAsync<LoginResponseDTO>();

                // Create claims
                var claims = new List<Claim>
                {
                        new Claim(ClaimTypes.Name, loginData.Email),
                        new Claim(ClaimTypes.Role, loginData.UserRole)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(principal);

                // Redirect based on role
                if (loginData.UserRole == "Admin")
                    return RedirectToAction("AdminDashboard", "Dashboard");

                else if (loginData.UserRole == "Customer")
                    return RedirectToAction("UserDashboard", "Dashboard");

                else
                    return RedirectToAction("AccountLogin"); // fallback
            }

            // If login failed
            ModelState.AddModelError("", "Invalid login credentials");
            string newCode = CaptchaGenerator.GenerateCaptchaCode(5);
            HttpContext.Session.SetString("Captchacode", newCode);
            model.CaptchaI = newCode;
            model.CaptchaText = string.Empty;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction("AccountLogin");
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
                userRole = model.userRole ?? "Admin"
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
