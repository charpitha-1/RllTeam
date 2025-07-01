using FoodieMVC.DTO;
using FoodieMVC.Infra;
using Microsoft.AspNetCore.Mvc;

namespace FoodieMVC.Controllers
{
    [Route("FoodItems")]
    public class FoodItemsController : Controller
    {
        private readonly ClientHelper clientHelper;

        public FoodItemsController(ClientHelper client)
        {
            clientHelper = client;
        }

        // Route: GET /FoodItems/ViewFoodItem/7
        [HttpGet("ViewFoodItem/{id}")]
        public async Task<IActionResult> ViewFoodItem(int id)
        {
            var res = await clientHelper.GetData<GetFoodItemDTO>($"FoodItem/{id}");
            var foodItems = res?.result ?? new List<FoodItemDTO>();  // Null check

            return View(foodItems);
        }
    }
}
