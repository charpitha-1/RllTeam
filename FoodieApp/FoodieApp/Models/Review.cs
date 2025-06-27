using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodieApp.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        [Required]
        public int Rating {  get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [ForeignKey("Users")]
        public int UserId { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public string comments { get; set; }
        public User Users { get; set; }
        public Order Order { get; set; }

        //[ForeignKey("RestId")]
        //public int RestaurantId { get; set; }
        //public Restaurant RestId { get; set; }
    }
}
