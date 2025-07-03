using FoodieApp.DBAccess;
using FoodieApp.DTO;
using FoodieApp.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;

namespace FoodieNTest
{
    public class DeleteLocationTest
    {
        private DbContextOptions<FoodieDbContext> _options;
        private FoodieDbContext _context;
        private DbAccess _dbAccess;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<FoodieDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new FoodieDbContext(_options);
            _dbAccess = new DbAccess(_context);
        }

        [TearDown]
        public void Cleanup()
        {
            _context.Dispose();
        }

        [Test]
        public void DeleteLocation_ValidId_RemovesLocationAndReturnsTrue()
        {
            // Arrange
            var location = new Location
            {
                LocationId = 100,
                City = "Chennai",
                Area = "Anna Nagar",
                PostalCode = "600040"
            };

            _context.Locations.Add(location);
            _context.SaveChanges();

            // Act
            var result = _dbAccess.DeleteLocation(location.LocationId);
            var deleted = _context.Locations.Find(location.LocationId);

            // Assert
            Assert.That(result, Is.True);
            Assert.That(deleted, Is.Null);
        }
    }
}
