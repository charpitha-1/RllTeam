using FoodieMVC.DTO;
using Microsoft.AspNetCore.Mvc;

namespace FoodappMVC.Controllers
{
    public class RestaurantController : Controller
    {

        private readonly IHttpClientFactory httpClientFactory;

        public RestaurantController(IHttpClientFactory _httpClientFactory)
        {
            httpClientFactory = _httpClientFactory;
        }
        //-----Viewing Restaurant---------

        [HttpGet]
        public async Task<IActionResult> ViewRestaurants()
        {
            var client = httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7059/api/Restaurant");

            if (!response.IsSuccessStatusCode)
                return View(new List<RestuarantDTO>());

            var data = await response.Content.ReadFromJsonAsync<GetRestaurantDTO>();

            // null check for result
            var result = data?.result ?? new List<RestuarantDTO>();
            return View(result);
        }

        // ----Adding Restaurant-----
        [HttpGet]
        public IActionResult AddRestaurant()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddRestaurant(RestuarantDTO model)
        {
            var client = httpClientFactory.CreateClient();
            var apiUrl = "https://localhost:7059/api/Restaurant";

            var response = await client.PostAsJsonAsync(apiUrl, model);
            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Restaurant added successfully!";
                return RedirectToAction("ViewRestaurants");
            }
            var error = await response.Content.ReadAsStringAsync();

            ModelState.AddModelError("", $"Something went wrong.: {error}");
            return View(model);
        }

        //-----Editing Restaurant-------------------

        // GET: Load restaurant for editing
        [HttpGet]
        public async Task<IActionResult> EditRestaurant(int id)
        {
            var client = httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7059/api/Restaurant/{id}");

            if (!response.IsSuccessStatusCode)
                return RedirectToAction("ViewRestaurants");

            var Strrestaurant = await response.Content.ReadAsStringAsync();//.ReadFromJsonAsync<EditRestaurantDTO>();
            var ResObj=System.Text.Json.JsonSerializer.Deserialize<EditRestaurantDTO>(Strrestaurant);

            return View(ResObj.result);
        }

        // POST: Submit updated restaurant
        [HttpPost]
        public async Task<IActionResult> EditRestaurant(RestuarantDTO model)
        {
            var client = httpClientFactory.CreateClient();
            var response = await client.PutAsJsonAsync($"https://localhost:7059/api/Restaurant/{model.restaurantId}", model);

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Restaurant updated successfully!";
                return RedirectToAction("ViewRestaurants");
            }

            var error = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError("", $"Update failed: {error}");
            return View(model);
        }


        //--------Deleting restaurant

        // GET: Show delete confirmation view
        [HttpGet]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            var client = httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7059/api/Restaurant/{id}");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Failed to load restaurant.";
                return RedirectToAction("ViewRestaurants");
            }

            var strRestaurant = await response.Content.ReadAsStringAsync();
            var resObj = System.Text.Json.JsonSerializer.Deserialize<EditRestaurantDTO>(strRestaurant);

            return View(resObj.result); // Pass restaurant to confirmation view
        }

        // POST: Perform delete operation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRestaurantConfirmed(int restaurantId)
        {
            var client = httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"https://localhost:7059/api/Restaurant/{restaurantId}");

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Restaurant deleted successfully!";
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                TempData["Error"] = $"Delete failed: {error}";
            }

            return RedirectToAction("ViewRestaurants");
        }




    }
}
