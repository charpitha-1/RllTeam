using FoodappMVC.DTO;
using Microsoft.AspNetCore.Mvc;

namespace FoodappMVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public OrderController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> ViewOrders()
        {
            var client = httpClientFactory.CreateClient();

            // Use full API URL directly here
            var response = await client.GetAsync("https://localhost:7059/api/OrderDetails"); // Replace with your actual URL

            if (!response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Failed to retrieve orders.";
                return View(new List<OrderDetailsDTO>());
            }

            var orders = await response.Content.ReadFromJsonAsync<List<OrderDetailsDTO>>();

            if (orders == null || !orders.Any())
            {
                TempData["Message"] = "No orders found.";
                return View(new List<OrderDetailsDTO>());
            }

            return View(orders);
        }

        [HttpGet]
        public async Task<IActionResult> EditOrder(int id)
        {
            var client = httpClientFactory.CreateClient();

            // Use full API URL with the order ID
            var response = await client.GetAsync($"https://localhost:7059/api/OrderDetails/{id}");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Failed to retrieve order details.";
                return RedirectToAction("ViewOrders"); // Redirecting back if order fetch fails
            }

            var order = await response.Content.ReadFromJsonAsync<OrderDetailsDTO>();

            if (order == null)
            {
                TempData["Message"] = "Order not found.";
                return RedirectToAction("ViewOrders");
            }

            return View(order); // Pass to EditOrder.cshtml view
        }

        [HttpPost]
        public async Task<IActionResult> EditOrder(OrderDetailsDTO order)
        {
            var client = httpClientFactory.CreateClient();

            // Use full API URL for the PUT request with orderId in the route
            var response = await client.PutAsJsonAsync($"https://localhost:7059/api/OrderDetails/{order.OrderId}", order);

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Order updated successfully.";
                return RedirectToAction("ViewOrders");
            }
            else
            {
                TempData["Error"] = "Failed to update order.";
                return View(order); // Return same view to show validation or error
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            var client = httpClientFactory.CreateClient();

            var response = await client.DeleteAsync($"https://localhost:7059/api/OrderDetails/{orderId}");

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Order deleted successfully.";
            }
            else
            {
                TempData["Error"] = "Failed to delete the order.";
            }

            return RedirectToAction("ViewOrders");
        }




    }
}
