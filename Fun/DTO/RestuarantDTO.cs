namespace FoodieMVC.DTO
{
    public class RestuarantDTO
    {
        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public string RestaurantAddress { get; set; }
        public int? Likes { get; set; }
        public double Rating { get; set; }
        public DateTime OpeningTime { get; set; }
        public DateTime CloseTime { get; set; }
        public int RestLocId {  get; set; }
        
    }
    public class GetRestaurantDTO
    {
        public List<RestuarantDTO> result {  get; set; }
    }
}
