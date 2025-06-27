namespace FoodieApp.Models
{
    public class Location
    {
        public int LocationId { get; set; }
       
        public string City { get; set; }
        public string Area { get; set; }
        public string PostalCode { get; set; }
        //public List<Address> addresses { get; set; }
        public List<Restaurant> restaurants { get; set; }
        //public List<Driver> drivers { get; set; }

    }
}
