using FoodieApp.DBAccess;
using FoodieApp.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodieApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly DbAccess dbAccess;

        public ReviewController(DbAccess db)
        {
            dbAccess = db;
        }



        [HttpPost]
        public IActionResult AddReview(ReviewDTO data)
        {
            if (data == null)
                return BadRequest(new { result = false, message = "Invalid data" });

            bool isAdded = dbAccess.AddReview(data);

            if (!isAdded)
                return BadRequest(new { result = false });

            return Ok(new { result = true, message = "Review added successfully" });
        }

        [HttpGet]
        public IActionResult GetAllReviews()
        {
            var reviews = dbAccess.GetAllReviews();
            if (reviews == null || !reviews.Any())
                return NotFound(new { result = false, message = "No reviews found" });

            return Ok(new { result = true, data = reviews });
        }

        [HttpGet("{foodItemId}")]
        public IActionResult GetReviewsByFoodItem(int foodItemId)
        {
            var reviews = dbAccess.GetReviewsByFoodItem(foodItemId);
            if (reviews == null)
                return NotFound(new { result = false, message = "No reviews found for this food item" });

            return Ok(new { result = true, data = reviews });
        }

        [HttpPut("{foodItemId}")]
        public IActionResult UpdateReviewByFoodItemId(int foodItemId, ReviewDTO data)
        {
            if (foodItemId != data.FoodItemId)
                return BadRequest(new { result = false, message = "FoodItemId mismatch" });

            bool isUpdated = dbAccess.UpdateReviewByFoodItemId(data);
            if (!isUpdated)
                return NotFound(new { result = false, message = "Review not found or update failed" });

            return Ok(new { result = true, message = "Review updated successfully" });
        }

        [HttpDelete("{foodItemId}")]
        public IActionResult DeleteReviewByFoodItemId(int foodItemId)
        {
            bool isDeleted = dbAccess.DeleteReviewByFoodItemId(foodItemId);
            if (!isDeleted)
                return NotFound(new { result = false, message = "Review not found or already deleted" });

            return Ok(new { result = true, message = "Review deleted successfully" });
        }









    }
}

