using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;
using VisitorManagement.Data;
using VisitorManagement.DTO;

namespace VisitorManagement.Controllers
{
    public class AccountController : Controller
    {
        private VisitorManagementContext context;
        public AccountController(VisitorManagementContext _context)
        {
            context = _context;
        }
        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            if (ModelState.IsValid)
            {
                UserRole ui = context.userRoles.Where(u => u.Email == model.Email && u.Password == model.Password).FirstOrDefault();
                if (ui!=null)
                {
                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Name, ui.Email));
                    claims.Add(new Claim(ClaimTypes.Role, ui.Role));
                   
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity
                        (claims, CookieAuthenticationDefaults.AuthenticationScheme);
                  
                    ClaimsPrincipal principle = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(principle);
                    string sUserInfo = JsonSerializer.Serialize(ui);
                   
                    HttpContext.Session.SetString("UserRole", sUserInfo);
                    return RedirectToAction("HostView", "HostManager");
                }
                else
                {   
                    return RedirectToAction("VisitorView", "VisitorManger");
                }
            }
            return View();
        }
    }
}
