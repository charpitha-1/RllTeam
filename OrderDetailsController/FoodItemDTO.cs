using FoodieApp.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodieApp.DTO
{
    public class FoodItemDTO
    {
        public int FoodItemId { get; set; }  // Required for PUT/DELETE
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; } = 50;

        public int RestaurantId { get; set; } // Foreign key to restaurant
    }
}

