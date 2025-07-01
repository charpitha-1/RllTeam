namespace FoodieMVC.DTO
{
    public class LocationDTO
    {
        public int locationId { get; set; }  // For reads; ignored on create
        public string city { get; set; }
        public string area { get; set; }
        public string postalCode { get; set; }
    }
    public class GetLocationDTO
    {
        public List<LocationDTO> result { get; set; }
    }
}
