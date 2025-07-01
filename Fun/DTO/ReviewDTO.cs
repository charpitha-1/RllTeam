namespace FoodieMVC.DTO
{
    public class ReviewDTO
    {
        public int reviewId { get; set; }
        public int? rating { get; set; }
        public DateTime createdAt { get; set; }
        public string comments { get; set; }
        public int userId { get; set; }
        public int orderId { get; set; }
        public int foodItemId { get; set; }
    }
}
