using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;

namespace FoodieApp.Models
{
    public class Address
    {
        public int AddressId { get; set; }
        public string Street { get; set; }
        public  string City { get; set; } 
        public string PostalCode { get; set; }
        [ForeignKey("User")]
        public int  UserId { get; set; }
        public User User { get; set; }
       // public int LocationId { get; set; }
        //public Location Location { get; set; }
        public List<Restaurant> restaurants { get; set; }
        public List<Order> Orders { get; set; }

    }
}
