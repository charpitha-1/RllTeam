namespace FoodieMVC.DTO
{
    public class PopularFood
    {
        public string FoodName { get; set; }
        public int QuantityOrdered { get; set; }
    }
    // DTO to receive order data from API
    

    public class OrderLineItemDTO
    {
        public int foodItemId { get; set; }
        public int qty { get; set; }
        public double unitPrice { get; set; }
    }

    public class ApiResponse<T>
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public List<T> Result { get; set; }
    }


}
