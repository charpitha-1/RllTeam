using FoodieApp.Models;

namespace FoodMVC1.DTO
{
    public class Search
    {

        public List<string> Cities { get; set; }
        public List<string> Areas { get; set; }
        public List<string> PostalCodes { get; set; }
        public List<string> RestaurantNames { get; set; }
        public List<FoodItemDTOs> FoodItems { get; set; }
        public string SelectedCity { get; set; }
        public string SelectedArea { get; set; }
        public string SelectedPostalCode { get; set; }
        public string SelectedRestaurantName { get; set; }

    }
    public class FoodItemDTOs
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string RestaurantName { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
    }

}
