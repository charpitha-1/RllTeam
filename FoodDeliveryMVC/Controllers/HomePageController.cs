using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryMVC.Controllers
{
    public class HomePageController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
