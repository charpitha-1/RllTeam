namespace FoodieMVC.DTO
{
    public class RestuarantDTO
    {
        public int restaurantId { get; set; }
        public string restaurantName { get; set; }
        public string restaurantAddress { get; set; }
        public int? likes { get; set; }
        public double rating { get; set; }
        public DateTime openingTime { get; set; }
        public DateTime closeTime { get; set; }
        public int restLocId {  get; set; }
        
    }
    public class GetRestaurantDTO
    {
        public List<RestuarantDTO> result {  get; set; }
    }
}
