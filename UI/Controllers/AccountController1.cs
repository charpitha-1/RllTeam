using FoodieCoreMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodieCoreMVC.Controllers
{
    public class AccountController1 : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // TODO: Add login logic (authenticate user)

            // On success
            return RedirectToAction("Index", "Home");

            // On failure, add model error
            // ModelState.AddModelError("", "Invalid email or password");
            // return View(model);
        }
    }
}
