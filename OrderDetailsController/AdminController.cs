using System.Text;
using Fooddie.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fooddie.Controllers
{
    public class AdminController : Controller
    {
        fooddieContext ob = new fooddieContext();
        public IActionResult Home()
        {
            int user_count = ob.UserRegs.ToList().Count();
            int restaurant_Count = ob.Restaurants.Where(t => t.Status == "approved").ToList().Count();
            int delivery_Count = ob.DeliverRegs.ToList().Count();
            int pending_List_count = ob.Restaurants.Count(t => t.Status == "Pending");
            ViewBag.user_count = user_count;
            ViewBag.restaurant_count = restaurant_Count;
            ViewBag.delivery_count = delivery_Count;
            ViewBag.pending_List_count = pending_List_count;
            return View();
        }
       
       
        public IActionResult DeleteUser(int id)
        {
            var user = ob.UserRegs.FirstOrDefault(t => t.UserId == id);
            if (user != null)
            {
                ob.UserRegs.Remove(user);
                if (ob.SaveChanges() > 0)
                {
                    ViewBag.i = "User deleted successfully";
                }
            }
            else
            {
                ViewBag.i = "Unable to delete the user";
            }
            return View();
        }
        [HttpGet]
        public IActionResult UpdateUser(int id)
        {
            var user = ob.UserRegs.FirstOrDefault(u => u.UserId == id);
            return View(user);
        }
        [HttpPost]
        public IActionResult UpdateUser(UserReg r)
        {
            var existing = ob.UserRegs.FirstOrDefault(u => u.UserId == r.UserId);
            if (existing != null)
            {
                existing.Fullname = r.Fullname;
                existing.Email = r.Email;
                existing.Phone = r.Phone;
                existing.Area = r.Area;
                existing.City = r.City;
                existing.Age = r.Age;
                ob.UserRegs.Update(existing);
                ViewBag.i = ob.SaveChanges();
            }
            return View(r);
        }
        public IActionResult ViewUsers()
        {
            //int user_count = ob.UserRegs.ToList().Count();
            //ViewBag.user_count = user_count;
            var b = ob.UserRegs.ToList();
            return View(b);
        }
        [HttpGet]
        public IActionResult ViewUserById()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ViewUserbyId(int userid)
        {
            UserReg b = new UserReg();
            b = ob.UserRegs.FirstOrDefault(t => t.UserId == userid);
            return View(b);
        }
        public IActionResult AdminProfile()
        {
            var res = from t in ob.AdminRegs  select t;
            return View(res.ToList());
        }

        public IActionResult ViewRestaurants()
        {
            int restaurant_count = ob.Restaurants.ToList().Count();
            ViewBag.user_count = restaurant_count;
            var b = ob.Restaurants.ToList();
            return View(b);
        }
        [HttpGet]
        public IActionResult ViewRestaurantsById()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ViewRestaurantsById(int restid)
        {
            Restaurant b = new Restaurant();
            b = ob.Restaurants.FirstOrDefault(t => t.RestaurantId == restid);
            return View(b);
        }
        public IActionResult DeleteRestaurant(int restid)
        {
            var user = ob.Restaurants.FirstOrDefault(t => t.RestaurantId == restid);
            if (user != null)
            {
                ob.Restaurants.Remove(user);
                ViewBag.i = ob.SaveChanges();
            }
            return View(ViewBag.i);
        }
        [HttpGet]
        public IActionResult UpdateRestaurant(int Restid)
        {
            var res = ob.Restaurants.FirstOrDefault(u => u.RestaurantId == Restid);
            return View(res);
        }
        [HttpPost]
        public IActionResult UpdateRestaurant(Restaurant r)
        {
            var current = ob.Restaurants.FirstOrDefault(u => u.RestaurantId == r.RestaurantId);
            if (current != null)
            {
                current.RestName = r.RestName;
                current.Email = r.Email;
                current.PhoneNo = r.PhoneNo;
                current.Area = r.Area;
                current.City = r.City;
                current.Description = r.Description;
                ViewBag.i = ob.SaveChanges();
                ModelState.Clear();
            }
            return View(r);
        }
        //[HttpGet]
        //public IActionResult AddRestaurant()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public IActionResult AddRestaurant(Restaurant model)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        try
        //        {
        //            ob.Restaurants.Add(model);
        //            if (ob.SaveChanges() > 0)
        //            {
        //                ViewBag.e = "Restaurant added successfully";
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            ViewBag.e = ex.Message;
        //        }
        //    }
        //    else
        //    {
        //        ViewBag.e = "add denied";
        //    }
        //    return View();
        //}
        [HttpGet]
        public IActionResult EditAdmin(int id)
        {
            var res = ob.AdminRegs.FirstOrDefault(u => u.UserId == id);
            return View(res);
        }
        [HttpPost]
        public IActionResult EditAdmin(AdminReg r)
        {
            var current = ob.AdminRegs.FirstOrDefault(u => u.UserId == r.UserId);
            if (current != null)
            {
                current.Fullname = r.Fullname;
                current.Email = r.Email;
                current.Username = r.Username;
                current.Email = r.Email;
                current.Phone = r.Phone;
                current.City = r.City;
                current.Area = r.Area;
                current.Sq1 = r.Sq1;
                current.Sq2 = r.Sq2;
                ob.AdminRegs.Update(current);
                ViewBag.i = ob.SaveChanges();
                ModelState.Clear();
            }
            return View(r);
        }

        public IActionResult RestaurantApproval()
        {
            var res = ob.Restaurants.Where(u => u.Status == "pending");
            return View(res.ToList());
        }
        public IActionResult UpdateRestaurantStatus(int restid, string status)
        {
            var res = ob.Restaurants.FirstOrDefault(t => t.RestaurantId == restid);
            if (res != null)
            {
                res.Status = status;
                ob.SaveChanges();
                TempData["Message"] = $"Status set to '{status}' successfully.";
            }
            else
            {
                TempData["Message"] = "Restaurant not found.";
            }
            return RedirectToAction("RestaurantApproval"); // or your actual listing page
        }
        public IActionResult ViewAgent()
        {
            var res = ob.DeliverRegs.ToList();

            return View(res);
        }
        [HttpGet]
        public IActionResult ViewAgentById()
        {

            return View();
        }
        [HttpPost]
        public IActionResult ViewAgentById(int id)
        {
            DeliverReg b = new DeliverReg();
            b = ob.DeliverRegs.FirstOrDefault(t => t.UserId == id);
            return View(b);
        }
        [HttpGet]
        public IActionResult UpdateAgent(int id)
        {
            var user = ob.DeliverRegs.FirstOrDefault(u => u.UserId == id);
            return View(user);
        }
        [HttpPost]
        public IActionResult UpdateAgent(DeliverReg r)
        {
            var existing = ob.DeliverRegs.FirstOrDefault(u => u.UserId == r.UserId);
            if (existing != null)
            {
                existing.Fullname = r.Fullname;
                existing.Email = r.Email;
                existing.Phone = r.Phone;
                existing.Area = r.Area;
                existing.City = r.City;
                existing.Age = r.Age;
                existing.DlNo = r.DlNo;
                existing.VehicleNo = r.VehicleNo;
                ob.DeliverRegs.Update(existing);
                ViewBag.i = ob.SaveChanges();
            }
            return View(r);
        }
        public IActionResult DeleteAgent(int id)
        {
            var user = ob.DeliverRegs.FirstOrDefault(t => t.UserId == id);
            if (user != null)
            {
                ob.DeliverRegs.Remove(user);
                if (ob.SaveChanges() > 0)
                {
                    ViewBag.i = "User deleted successfully";
                }
            }
            else
            {
                ViewBag.i = "Unable to delete the user";
            }
            return View();
        }
    }
}

    

