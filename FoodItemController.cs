using FoodieApp.DBAccess;
using FoodieApp.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodieApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodItemController : ControllerBase
    {
        private readonly DbAccess dbAccess;

        public FoodItemController(DbAccess db)
        {
            dbAccess = db;
        }

        [HttpPost]
        public IActionResult AddFoodItem(FoodItemDTO data)
        {
            var fi = dbAccess.AddFoodItem(data);
            return Ok(new { result = "FoodItem added successfully", fi });
        }

        [HttpGet("{id}")]
        public IActionResult GetFoodItem(int id)
        {
            var fi = dbAccess.GetFoodItemById(id);
            if (fi == null) return NotFound(new { result = false, message = "FoodItem not found" });
            return Ok(new { result = true, fi });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateFoodItem(int id, FoodItemDTO data)
        {
            if (id != data.FoodItemId)
                return BadRequest(new { result = false, message = "ID mismatch" });

            var updated = dbAccess.UpdateFoodItem(data);
            if (!updated) return NotFound(new { result = false, message = "FoodItem not found" });

            return Ok(new { result = true, message = "FoodItem updated successfully" });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFoodItem(int id)
        {
            var deleted = dbAccess.DeleteFoodItem(id);
            if (!deleted) return NotFound(new { result = false, message = "FoodItem not found" });

            return Ok(new { result = true, message = "FoodItem deleted successfully" });
        }
    }
}

