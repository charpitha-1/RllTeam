
namespace FoodieMVC.DTO
{
    public class FoodItemDTO
    {
        public string name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public string restaurantName { get; set; }
        public string city { get; set; }
        public string postalCode { get; set; }
        public string location { get; set; }
    }
    public class GetFoodItemDTO
    {
        public List<FoodItemDTO> result { get; set; }

        public static implicit operator GetFoodItemDTO(List<FoodItemDTO> v)
        {
            throw new NotImplementedException();
        }
    }
}
