namespace FoodappMVC.DTO
{
    public class OrderDetailsDTO
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public double OrderTotal { get; set; }
        public double Discount { get; set; }
        public double Gst { get; set; }
        public double FinalAmount { get; set; }
        public string OrderStatus { get; set; }
        public string DeliveredBy { get; set; }
        public int UserId { get; set; }
        public int AddressId { get; set; }
        public int RestId { get; set; }
        public DateTime? ScheduleDeliveryAt { get; set; }

        public List<OrderLineItemDTO> OrderLineItems { get; set; } = new List<OrderLineItemDTO>();
    }
    public class OrderLineItemDTO
    {
        public int FoodItemId { get; set; }
        public int Qty { get; set; }
        public double UnitPrice { get; set; }
    }
}
