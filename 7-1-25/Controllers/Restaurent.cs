using Microsoft.AspNetCore.Mvc;

namespace FoodieCoreMVC.Controllers
{
    public class Restaurent : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
