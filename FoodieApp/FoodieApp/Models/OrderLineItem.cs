using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodieApp.Models
{
    public class OrderLineItem
    {
        [Key] 
        public int OrderItemId { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        
        public Order Order { get; set; }

        [ForeignKey("FoodItems")]
        public int FoodItemId { get; set; }
        
        public int Quantity { get; set; }

       // public int RestaurantId { get; set; }
        //[Column(TypeName = "decimal(8,2)")]
        //public double Price { get; set; }
       
        public List<FoodItem>  FoodItems { get; set; }
    }
}
