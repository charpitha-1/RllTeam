
using System.Text.Json.Serialization;

namespace FoodieMVC.DTO
{
    public class FoodItemDTO
    {
        public int foodItemId { get; set; }  // Required for PUT/DELETE
        public string name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; } = 50;

        public int restaurantId { get; set; }

    }
    public class GetFoodItemDTO
    {
        [JsonPropertyName("result")]
        public List<FoodItemDTO> result { get; set; }

        
    }
}
