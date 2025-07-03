using FoodieApp.DBAccess;
using FoodieApp.DTO;
using FoodieApp.Models;
using Microsoft.EntityFrameworkCore;
using System;

using NUnit.Framework;
namespace FoodieNTest
{
    public class DeleteRestaurantTest
    {
        private DbContextOptions<FoodieDbContext> _options;
        private FoodieDbContext _context;
        private DbAccess _dbAccess;

        [SetUp]
        public void Setup()
        {
            // Setup an in-memory database
            _options = new DbContextOptionsBuilder<FoodieDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())

                .Options;

            _context = new FoodieDbContext(_options);
            _dbAccess = new DbAccess(_context);
        }

        [TearDown]
        public void Cleanup()
        {
            _context.Dispose(); // ✅ Properly disposes the DbContext
        }

        [Test]
        public void DeleteRestaurant_ValidId_RemovesRestaurantAndReturnsTrue()
        {
            // Arrange: Add a restaurant to delete
            var restaurant = new Restaurant
            {
                RestaurantId = 1,
                RestaurantName = "Biryani House",
                RestaurantAddress = "456 Masala Street",
                Likes = 200,
                Rating = 4.8,
                OpeningTime = DateTime.Today.AddHours(11),
                CloseTime = DateTime.Today.AddHours(23),
                RestLocId = 501
            };

            _context.Restaurants.Add(restaurant);
            _context.SaveChanges();

            // Act
            var result = _dbAccess.DeleteRestaurant(restaurant.RestaurantId);
            var deleted = _context.Restaurants.Find(restaurant.RestaurantId);

            // Assert
            Assert.That(result, Is.True);
            Assert.That(deleted, Is.Null); // restaurant should be gone
        }


    }
}
