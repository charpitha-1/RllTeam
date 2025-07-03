using FoodieApp.DBAccess;
using FoodieApp.DTO;
using FoodieApp.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;

namespace FoodieNTest
{
    public class ReviewTests
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
        public void AddReview_ValidReview_ReturnsTrue_AndIsStored()
        {
            // Arrange
            var dto = new ReviewDTO
            {
                rating = 5,
                createdAt = DateTime.UtcNow,
                comments = "Great food!",
                userId = 1,
                orderId = 1,
                foodItemId = 1
            };

            // Act
            var result = _dbAccess.AddReview(dto);
            var savedReview = _context.Reviews.FirstOrDefault(r => r.UserId == 1 && r.OrderId == 1 && r.FoodItemId == 1);

            // Assert
            Assert.That(result, Is.True);
            Assert.That(savedReview, Is.Not.Null);
            Assert.That(savedReview.Rating, Is.EqualTo(5));
            Assert.That(savedReview.Comments, Is.EqualTo("Great food!"));

        }
    }
}
