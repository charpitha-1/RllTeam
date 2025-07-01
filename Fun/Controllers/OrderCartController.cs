using Microsoft.AspNetCore.Mvc;

namespace FoodieMVC.Controllers
{
    public class OrderCartController : Controller
    {
        public IActionResult ViewUserCart()
        {
            return View();
        }
    }
}
