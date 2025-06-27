using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodieApp.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required,StringLength(30)]
        public string FirstName { get; set; }
        [Required, StringLength(30)]
        public string LastName { get; set; }
        [Required,DataType(DataType.EmailAddress)] 
        
        public string Email{ get; set; }
        [Required, DataType(DataType.Password)]
        public string Password{ get; set; }
        [Required,DataType(DataType.PhoneNumber)]
        public string PhoneNumber{ get; set; }
        //public DateTime RegisteredDate { get; set; }
        //public bool IsFirstOrder { get; set; }
        //[Required]
        //public string CaptchaInput { get; set; }
        [ForeignKey("addresses")]
        public int AddressId { get; set; }
        public List<Address> addresses { get; set; }
        public List<Order> Orders { get; set; }
        public List<Review> Reviews { get; set; } 
       



    }
}
