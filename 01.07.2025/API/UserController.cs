using FoodappMVC.DTO;
using FoodieApp.DBAccess;
using FoodieApp.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FoodieApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DbAccess dbAccess;

        public UserController(DbAccess db)
        {
            dbAccess = db;
        }

        //[HttpPost]
        //public IActionResult AddUser(UserDTO data)
        //{
        //    var result = dbAccess.AddUser(data);
        //    bool status = result != null;
        //    return Ok(new { result  = "Success in Adding User" });
        //}

        [HttpPost]
        public IActionResult AddUser(UserDTO data)
        {
            var result = dbAccess.AddUser(data);

            if (result == null)
                return BadRequest("Failed to add user (duplicate email or DB issue)");

            return Ok(new { result = "Success in Adding User" });
        }


        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var userList = dbAccess.GetAllUsers();
            return Ok(userList);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = dbAccess.GetUserById(id);
            if (user == null)
                return NotFound(new { result = false, message = "User not found" });

            return Ok(new { result = true, user });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, UserDTO data)
        {
            bool status = dbAccess.UpdateUser(id, data);
            if (!status)
                return NotFound(new { result = false, message = "User not found" });

            return Ok(new { result = "Updated successfully" });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            bool status = dbAccess.DeleteUser(id);
            if (!status)
                return NotFound(new { result = false, message = "User not found" });

            return Ok(new { result = "Successfully Deleted the User" });
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginRequestDTO model)
        {
            var users = dbAccess.GetAllUsers();

            var matchedUser = users
                .FirstOrDefault(u => u.email == model.Email && u.password == model.Password);

            if (matchedUser == null)
                return Unauthorized("Invalid email or password");

            // Normalize role casing
            var role = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(matchedUser.userRole.ToLower());

            var response = new LoginResponseDTO
            {
                Email = matchedUser.email,
                UserRole = role
            };

            return Ok(response);
        }

    }
}
