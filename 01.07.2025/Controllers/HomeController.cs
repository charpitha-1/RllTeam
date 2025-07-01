using Microsoft.AspNetCore.Mvc;

namespace FoodappMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Welcome()
        {
            return View();
        }
    }
}
