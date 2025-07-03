using FoodieApp.DBAccess;
using FoodieApp.DTO;
using FoodieApp.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodieNTest
{
    public class GetAllLocationsTest
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

            // Seed test data
            _context.Locations.AddRange(
                new Location { City = "Chennai", Area = "T Nagar", PostalCode = "600017" },
                new Location { City = "Bangalore", Area = "Indiranagar", PostalCode = "560038" }
            );
            _context.SaveChanges();
        }

        [TearDown]
        public void Cleanup()
        {
            _context.Dispose();
        }

        [Test]
        public void GetAllLocations_ReturnsCorrectLocationList()
        {
            // Act
            var result = _dbAccess.GetAllLocations();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.Any(l => l.City == "Chennai" && l.Area == "T Nagar"));
            Assert.That(result.Any(l => l.City == "Bangalore" && l.Area == "Indiranagar"));
        }
    }
}
