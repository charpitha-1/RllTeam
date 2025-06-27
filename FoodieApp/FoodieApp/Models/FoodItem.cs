using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodieApp.Models
{
    public class FoodItem
    {
        [Key]
        public int FoodItemId { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        [Column(TypeName = "decimal(8,2)")]
        public decimal Price { get; set; }
        [ForeignKey("RestId")]
        public int RestaurantId { get; set; }
        public Restaurant RestId { get; set; }
        //public List<OrderLineItem> Cart { get; set; }
    }
}