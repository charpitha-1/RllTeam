using Microsoft.AspNetCore.Mvc;
using FoodieCoreMVC.Models;
namespace FoodieCoreMVC.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            var model = new UserProfileViewModel
            {
                Name = "John Doe",
                Email = "john@example.com"
            };
            return View(model);
        }
    }
}
