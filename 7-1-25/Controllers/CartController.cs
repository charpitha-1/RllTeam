using Microsoft.AspNetCore.Mvc;
using FoodieCoreMVC.Models;
namespace FoodieCoreMVC.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            

            return View();
        }
    }
}
