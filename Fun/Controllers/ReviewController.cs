using FoodieApp.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace FoodieMVC.Controllers
{
    public class ReviewController : Controller
    {
        private readonly HttpClient _httpClient;

        public ReviewController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet]
        public IActionResult CreateReview(int orderId, int foodItemId, int userId)
        {
            var review = new ReviewDTO
            {
                OrderId = orderId,
                FoodItemId = foodItemId,
                UserId = userId
            };
            return View(review);
        }

        
        [HttpPost]
        public async Task<IActionResult> CreateReview(ReviewDTO review)
        {
            review.CreatedAt = DateTime.Now;

            var json = JsonSerializer.Serialize(review);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://your-api-url.com/api/reviews", content);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("ListReviews");

            ViewBag.Error = "Failed to submit review.";
            return View(review);
        }

        [HttpGet]
        public async Task<IActionResult> ListReviews()
        {
            var response = await _httpClient.GetAsync("https://your-api-url.com/api/reviews");

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Unable to load reviews.";
                return View(new List<ReviewDTO>());
            }

            var json = await response.Content.ReadAsStringAsync();
            var reviews = JsonSerializer.Deserialize<List<ReviewDTO>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return View(reviews);
        }
    }
}
