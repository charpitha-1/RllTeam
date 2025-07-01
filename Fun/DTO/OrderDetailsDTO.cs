using System.ComponentModel.DataAnnotations.Schema;

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
        [NotMapped] 
        public double deliveryCharge { get; set; }
        [NotMapped]
        public double distanceInKm { get; set; }
        public int userId { get; set; }
        public int addressId { get; set; }
        public int restId { get; set; }
        public DateTime? scheduleDeliveryAt { get; set; }

        public List<OrderLineItemDTO> OrderLineItems { get; set; }
    }
    public class GetOrderDetails
    {
        public List<OrderDetailsDTO> result {  get; set; }
    }
}
