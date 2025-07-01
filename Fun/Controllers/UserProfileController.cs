
using FoodieMVC.DTO;
using FoodieMVC.Infra;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FoodieMVC.Controllers
{
    public class UserProfileController : Controller
    {
        
        private readonly ClientHelper clientHelper;
        public UserProfileController(ClientHelper client)
        {
            clientHelper = client;
           
        }
        [HttpGet]
        public async Task<IActionResult> ViewProfile()
        {

            var res = await clientHelper.GetData<GetUserListDTO>("User");
            return View(res.result);


            
        }

    }
}

