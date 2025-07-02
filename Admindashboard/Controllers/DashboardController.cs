using FoodieMVC.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace FoodappMVC.Controllers
{

    public class DashboardController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public DashboardController(IHttpClientFactory _httpClientFactory)
        {
            httpClientFactory = _httpClientFactory;
        }


        [Authorize(Roles = "Admin")]
        public IActionResult AdminDashboard()
        {
            return View();
        }

        












        //[Authorize(Roles = "Customer")]
        //public IActionResult UserDashboard()
        //{
        //    Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
        //    Response.Headers["Pragma"] = "no-cache";
        //    Response.Headers["Expires"] = "0";
        //    return View();
        //}
    }
}
