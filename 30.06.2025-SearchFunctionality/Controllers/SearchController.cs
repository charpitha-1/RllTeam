using FoodieApp.DataServices;
using FoodieApp.Models;
using FoodMVC1.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text.Json;
using static FoodieApp.DTO.OrderDetailsDTO;

namespace FoodMVC1.Controllers
{
    public class SearchController : Controller
    {
        private readonly DataAccessServices dataService;
        private readonly FoodieDbContext context;
        public SearchController(DataAccessServices dataAccessServices, FoodieDbContext _context) { 
            dataService = dataAccessServices;
            context = _context;
        }
       

        
        public Search GetDropdownData()
        {
            return new Search
            {
                Cities = context.Locations.Select(l => l.City).Distinct().ToList(),
                Areas = context.Locations.Select(l => l.Area).Distinct().ToList(),
                PostalCodes = context.Locations.Select(l => l.PostalCode).Distinct().ToList(),
                RestaurantNames = context.Restaurants.Select(r => r.RestaurantName).Distinct().ToList()
            };
        }
        [HttpGet]
        public async Task<ActionResult> SearchFoodAsync()
        {
            var dropdownData = GetDropdownData();

            using var client = new HttpClient { 
                BaseAddress = new Uri("https://localhost:7059/api/") };

            var response = await client.GetAsync("Locations");
            if (!response.IsSuccessStatusCode)
            {
                // Handle error appropriately, e.g., log or return error view
                return View(dropdownData);
            }

            var respString = await response.Content.ReadAsStringAsync();

            var list = JsonSerializer.Deserialize<Search>(respString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var modelToPass = list ?? dropdownData;

            return View(modelToPass);
        }


        [HttpPost]
        public async Task<ActionResult> SearchFoodAsync(Search model)
        {
            // Reload dropdowns for redisplay
            var dropdownData = GetDropdownData();

            model.Cities = dropdownData.Cities;
            model.Areas = dropdownData.Areas;
            model.PostalCodes = dropdownData.PostalCodes;
            model.RestaurantNames = dropdownData.RestaurantNames;

            // Filter food items based on selected filters
            var query = context.FoodItems
     .Include(f => f.RestId)            // load related Restaurant entity
         .ThenInclude(r => r.Locations) // load Location from Restaurant
     .AsQueryable();

            if (!string.IsNullOrEmpty(model.SelectedCity))
                query = query.Where(f => f.RestId.Locations.City == model.SelectedCity);

            if (!string.IsNullOrEmpty(model.SelectedArea))
                query = query.Where(f => f.RestId.Locations.Area == model.SelectedArea);

            if (!string.IsNullOrEmpty(model.SelectedPostalCode))
                query = query.Where(f => f.RestId.Locations.PostalCode == model.SelectedPostalCode);

            if (!string.IsNullOrEmpty(model.SelectedRestaurantName))
                query = query.Where(f => f.RestId.RestaurantName == model.SelectedRestaurantName);

           
            model.FoodItems = await query.Select(f => new FoodItemDTOs
            {
                Name = f.Name,
                Description = f.Description,
                Price = f.Price,
                RestaurantName = f.RestId.RestaurantName,
                City = f.RestId.Locations.City,
                Area = f.RestId.Locations.Area
            }).ToListAsync();

            return View(model);
        }
        [HttpPost]
      
        public async Task<IActionResult> ViewFoodItemSearch()
        {
            var foodItems = await context.FoodItems
                .Include(f => f.RestId)                 
                    .ThenInclude(r => r.Locations)     // Load Location entity
                .Select(f => new FoodItemDTOs          // Map to DTO
                {
                    Name = f.Name,
                    Description = f.Description,
                    Price = f.Price,
                    RestaurantName = f.RestId.RestaurantName,
                    City = f.RestId.Locations.City,
                    Area = f.RestId.Locations.Area
                })
                .ToListAsync();

            // You can pass foodItems directly or wrap in a model that includes dropdowns if needed
            var model = new Search
            {
                FoodItems = foodItems,
                Cities = context.Locations.Select(l => l.City).Distinct().ToList(),
                Areas = context.Locations.Select(l => l.Area).Distinct().ToList(),
                PostalCodes = context.Locations.Select(l => l.PostalCode).Distinct().ToList(),
                RestaurantNames = context.Restaurants.Select(r => r.RestaurantName).Distinct().ToList()
            };

            return View(model);
        }

    }
}
