using FoodieMVC.DTO;
using FoodieMVC.Infra;
using Microsoft.AspNetCore.Mvc;

namespace FoodieMVC.Controllers
{
    public class AdminController : Controller
    {

        private readonly ClientHelper clientHelper;
        public AdminController(ClientHelper client)
        {
            clientHelper = client;

        }
        [HttpGet]
        public async Task<IActionResult> ViewRestaurants()
        {
            var res = await clientHelper.GetData<GetRestaurantDTO>("Restaurant");
            return View(res.result); // assuming the API returns { result: [ ... ] }
        }

        [HttpGet]
        public async Task<IActionResult> ViewLocations()
        {
            var res = await clientHelper.GetData<GetLocationDTO>("Location"); // endpoint should match your API route
            return View(res.result); // Pass the list to the view
        }
        [HttpGet("{orderId}")]
        public async Task<IActionResult> ViewOrder(int orderId)
        {
            var result = await clientHelper.GetData<OrderDetailsDTO>($"OrderDetails/{orderId}");
            if (result == null)
            {
                ViewBag.Error = "Order not found.";
                return View(); // Or redirect to error page
            }

            return View(result);
        }





    }
}
