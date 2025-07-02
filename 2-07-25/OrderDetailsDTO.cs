using FoodieApp.Models;

namespace FoodieMVC.DTO
{
    public class OrderDetailsDTO
    {
        public int orderId { get; set; }
        public DateTime orderDate { get; set; }
        public double orderTotal { get; set; }
        public double discount { get; set; }
        public double gst { get; set; }
        public double finalAmount { get; set; }
        public string orderStatus { get; set; }
        public string deliveredBy { get; set; }
        public int userId { get; set; }
        public int addressId { get; set; }
        public int restId { get; set; }
        public DateTime? scheduleDeliveryAt { get; set; }

        // List of related order line items
        public List<OrderLineItemDTO> orderLineItems { get; set; } = new List<OrderLineItemDTO>();
    }
    public class OrderLineItemDTO
    {
        public int foodItemId { get; set; }
        public int qty { get; set; }
        public decimal unitPrice
        {
            get
            {
                return foodItemId switch
                {
                    1 => 120,
                    2 => 90,
                    3 => 75,
                    4 => 150,
                    _ => 100 // default price
                };
            }
        }

    }
}

