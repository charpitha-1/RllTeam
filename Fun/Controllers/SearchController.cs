using FoodieApp.Models;
using FoodieMVC.DTO;
using FoodieMVC.Infra;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;

public class SearchController : Controller
{
    private readonly HttpClient client;

    public SearchController(IHttpClientFactory factory)
    {
        client = factory.CreateClient();
    }
    [HttpGet]
    public async Task<IActionResult> PopularFoods()
    {
        

        // 1. Get orders
        var ordersResponse = await client.GetStringAsync("http://localhost:44318/api/OrderDetails");
        var ordersWrapped = JsonSerializer.Deserialize<ApiResponse<OrderDetailsDTO>>(ordersResponse);
        var orders = ordersWrapped?.Result ?? new List<OrderDetailsDTO>();

        // 2. Get food items
        var foodsResponse = await client.GetStringAsync("http://localhost:44318/api/FoodItem");
        var foodsWrapped = JsonSerializer.Deserialize<ApiResponse<FoodItemDTO>>(foodsResponse);
        var foods = foodsWrapped?.Result ?? new List<FoodItemDTO>();

        // 3. Group & Count
        var popular = orders
            .SelectMany(o => o.OrderLineItems)
            .GroupBy(i => i.foodItemId)
            .Select(g =>
            {
                var food = foods.FirstOrDefault(f => f.foodItemId == g.Key);
                return new PopularFood
                {
                    FoodName = food?.name ?? "Unknown",
                    QuantityOrdered = g.Sum(x => x.qty)
                };
            })
            .OrderByDescending(x => x.QuantityOrdered)
            .ToList();

        return View(popular);
    }
    public async Task<IActionResult> DiscountedOrders()
    {
        string apiUrl = "https://your-api-endpoint.com/api/orders"; // Replace with real API URL

        var response = await client.GetAsync(apiUrl);
        if (!response.IsSuccessStatusCode)
        {
            ViewBag.Error = "Unable to fetch orders from API.";
            return View(new List<OrderDetailsDTO>());
        }

        var json = await response.Content.ReadAsStringAsync();
        var apiResponse = JsonSerializer.Deserialize<ApiResponse<OrderDetailsDTO>>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        var orders = apiResponse?.Result ?? new List<OrderDetailsDTO>();

        // Step 1: Group orders by user
        var groupedByUser = orders
            .GroupBy(o => o.userId)
            .ToDictionary(g => g.Key, g => g.OrderBy(o => o.orderDate).ToList());

        // Step 2: Apply discounts
        foreach (var userGroup in groupedByUser)
        {
            var userOrders = userGroup.Value;

            for (int i = 0; i < userOrders.Count; i++)
            {
                var order = userOrders[i];
                order.orderTotal = order.OrderLineItems.Sum(x => x.qty * x.unitPrice);

                if (i == 0) // First order
                {
                    order.discount = Math.Min(50, order.orderTotal * 0.10);
                }
                else // Regular user
                {
                    order.discount = order.orderTotal * 0.05;
                }

                order.gst = (order.orderTotal - order.discount) * 0.18;
                //delivery charges 
                order.deliveryCharge = order.distanceInKm <= 3 ? 20 : order.distanceInKm * 30;  
                order.finalAmount = order.orderTotal - order.discount + order.gst;
            }
        }

        var processedOrders = groupedByUser.SelectMany(g => g.Value).ToList();

        return View(processedOrders);
    }

}
