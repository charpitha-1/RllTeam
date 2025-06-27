using System.ComponentModel.DataAnnotations.Schema;

namespace FoodieApp.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public string PaymentMethod { get; set; }
        public double Amount { get; set; }
        public string PaymentStatus { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order Order { get; set; }

    }
}
