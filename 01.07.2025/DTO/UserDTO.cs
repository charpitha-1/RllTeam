namespace FoodieApp.DTO
{
    public class UserDTO
    {
        public int userId { get; set; } // For updates and reads
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; } // Used only in create/update
        public string phoneNumber { get; set; }
        public string userRole { get; set; } = "Customer";
    }
    public class GetUserDTO
    {
        public List<UserDTO> result { get; set; }
    }
}
