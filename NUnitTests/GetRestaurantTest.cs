using FoodieApp.DBAccess;
using FoodieApp.DTO;
using FoodieApp.Models;
using Microsoft.EntityFrameworkCore;
using System;

using NUnit.Framework;

namespace FoodieNTest
{
    public class GetRestaurantTest
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
        public void GetAllRestaurants_WhenCalled_ReturnsAllMappedDTOs()
        {
            // Arrange: Add sample restaurants directly to the in-memory database
            _context.Restaurants.AddRange(
                new Restaurant
                {
                    RestaurantId = 1,
                    RestaurantName = "The Spice Hub",
                    RestaurantAddress = "123 Curry Lane",
                    Likes = 150,
                    Rating = 4.5,
                    OpeningTime = DateTime.Today.Add(new TimeSpan(10, 0, 0)),
                    CloseTime = DateTime.Today.Add(new TimeSpan(22, 0, 0)),

                    RestLocId = 101
                },
                new Restaurant
                {
                    RestaurantId = 2,
                    RestaurantName = "Pasta Paradise",
                    RestaurantAddress = "456 Noodle Blvd",
                    Likes = 98,
                    Rating = 4.2,
                    OpeningTime = DateTime.Today.Add(new TimeSpan(11, 0, 0)),
                    CloseTime = DateTime.Today.Add(new TimeSpan(23, 0, 0)),

                   
                    RestLocId = 102
                }
            );
            _context.SaveChanges();

            // Act
            var result = _dbAccess.GetAllRestaurants();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));

            var first = result.First();
            Assert.That(first.RestaurantName, Is.EqualTo("The Spice Hub"));
            Assert.That(first.Rating, Is.EqualTo(4.5));
            Assert.That(first.RestLocId, Is.EqualTo(101));
        }

    }
}
