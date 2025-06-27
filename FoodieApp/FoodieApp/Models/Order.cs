using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodieApp.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public DateTime ScheduledDate { get; set; }
        [Column(TypeName = "decimal(8,2)")]

        [Required]
        public double OrderTotal { get; set; }

        [Column(TypeName = "decimal(8,2)")]
        public double Discount { get; set; }

        [Required]
        [Column(TypeName = "decimal(8,2)")]
        public double gst { get; set; }

        [Column(TypeName = "decimal(8,2)")]
        [Required]
        public double FinalAmount { get; set; }
        
        [Required]
        public string OrderStatus { get; set; }
        [ForeignKey("User")]
        public string deliveredBy { get; set; }

       [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("Restaurant")]
        public int RestId { get; set; }
        public Restaurant Restaurant { get; set; }
        
        public DateTime? ScheduleDeliveryAt { get; set; }
        public List<OrderLineItem> carts { get; set; }

        [ForeignKey("DeliveryPerson")]
        public int? DeliveryPersonId { get; set; }
        //public DeliveryDetails DeliveryDetails { get; set; }
        public Review Review { get; set; }


    }
}
