using Microsoft.AspNetCore.Mvc;

namespace FoodieCoreMVC.Controllers
{
    public class paymentController : Controller
    {
        public IActionResult payment()
        {
            return View();
        }
    }
}
