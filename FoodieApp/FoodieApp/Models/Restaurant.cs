using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodieApp.Models
{
    public class Restaurant
    {
        [Key]
        public int RestaurantId { get; set; }
        [Required]
        public string RestaurantName { get; set; }
        [Required]
        public string RestaurantAddress { get; set; }
        public int? Likes { get; set; }
      
        public double Rating { get; set; }
        [Required]
        public DateTime OpeningTime { get; set; }
        [Required]
        public DateTime CloseTime { get; set; }
        public List<FoodItem> FoodItems { get; set; }   
        public List<Order> Orders { get; set; }
        public List<Review> Reviews { get; set; }
        [ForeignKey("Locations")]
        public int RestLocId { get; set; }
        public Location Locations { get; set; }
    }
}
