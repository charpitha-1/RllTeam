using FoodieApp.Models;
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
            var res = await clientHelper.GetData<GetRestaurantDTO>("api/Restaurant");
            return View(res.result); // assuming the API returns { result: [ ... ] }
        }

        [HttpGet]
        public async Task<IActionResult> ViewLocations()
        {
            var res = await clientHelper.GetData<GetLocationDTO>("api/Location");
            // endpoint should match your API route
            return View(res.result); // Pass the list to the view
        }
        //[HttpGet("{orderId}")]
        //public async Task<IActionResult> ViewOrder(int orderId)
        //{
        //    var result = await clientHelper.GetData<OrderDetailsDTO>($"api/OrderDetails/{orderId}");
        //    if (result == null)
        //    {
        //        ViewBag.Error = "Order not found.";
        //        return View(); // Empty model to show error message
        //    }

        //    return View(result); // ✔ Passing a single order
        //}



        //=====================================================
        [HttpGet]
        public ActionResult Schedule()
        {
            return View(new OrderDetailsDTO()); // Show the form
        }

        [HttpPost]
        public async Task<ActionResult> Schedule(OrderDetailsDTO model)
        {
            if (!ModelState.IsValid)
            {
                // Log or set a breakpoint here
                return View(model); // form redisplayed silently
            }

            await clientHelper.PostData("Order/ScheduleDelivery", model);
            TempData["ScheduleTime"] = model.scheduleDeliveryAt?.ToString("f");

            return RedirectToAction("ScheduledConfirmation");
        }


        public ActionResult ScheduledConfirmation()
        {
            ViewBag.ScheduleTime = TempData["ScheduleTime"];
            return View();
        }

        //============================================================================
        [HttpGet("UserOrders/{userId}")]
        
        public IActionResult UserOrders(int userId)
        {
            var orders = clientHelper
                .GetData<List<OrderDetailsDTO>>($"OrderDetails/{userId}")
                .GetAwaiter().GetResult();

            return View("UserOrders", orders);
        }




        //[HttpGet("Admin/UserReviews/{userId}")]
        //public async Task<IActionResult> UserReviews(int userId)
        //{
        //    try
        //    {
        //        var reviews = await clientHelper.GetData<List<ReviewDTO>>($"User/{userId}");

        //        if (reviews == null || reviews.Count == 0)
        //        {
        //            ViewBag.Message = $"No reviews found for User ID {userId}.";
        //            return View(new List<ReviewDTO>()); // Return empty list to avoid null issues in view
        //        }

        //        return View(reviews);
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.Message = "Error fetching data: " + ex.Message;
        //        return View(new List<ReviewDTO>());
        //    }
        //}
        [HttpGet("Admin/UserReviews/{userId}")]
        public async Task<IActionResult> UserReviews(int userId)
        {
            // Use lowercase 'review' in URL if API controller is named ReviewController
            string apiEndpoint = $"api/review/User/{userId}";

            var reviews = await clientHelper.GetData<List<ReviewDTO>>(apiEndpoint);

            if (reviews == null || reviews.Count == 0)
            {
                ViewBag.Message = $"No reviews found for User ID {userId}.";
                return View(new List<ReviewDTO>());
            }

            return View(reviews);
        }






    }






}

