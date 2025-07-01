namespace FoodieMVC.DTO
{
    public class UserProfileDTO
    {
        public int userId { get; set; } // For updates and reads
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; } // Used only in create/update
        public string phoneNumber { get; set; }
        public string userRole { get; set; }
    }
    public class GetUserListDTO
    {
       public  List<UserProfileDTO> result {  get; set; }
    }
}
