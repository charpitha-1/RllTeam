using FoodieApp.DBAccess;
using FoodieApp.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;

namespace FoodieNTest
{
    public class DeleteReviewTest
    {
        private DbContextOptions<FoodieDbContext> _options;
        private FoodieDbContext _context;
        private DbAccess _dbAccess;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<FoodieDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new FoodieDbContext(_options);
            _dbAccess = new DbAccess(_context);

            // Seed a review
            _context.Reviews.Add(new Review
            {
                ReviewId = 1,
                Rating = 5,
                CreatedAt = DateTime.Now,
                Comments = "Fantastic!",
                UserId = 1,
                OrderId = 1,
                FoodItemId = 101
            });

            _context.SaveChanges();
        }

        [TearDown]
        public void Cleanup() => _context.Dispose();

        [Test]
        public void DeleteReview_ExistingFoodItemId_ReturnsTrueAndDeletes()
        {
            // Act
            var result = _dbAccess.DeleteReviewByFoodItemId(101);

            // Assert
            Assert.That(result, Is.True);
            Assert.That(_context.Reviews.Any(r => r.FoodItemId == 101), Is.False);
        }

        [Test]
        public void DeleteReview_NonExistingFoodItemId_ReturnsFalse()
        {
            // Act
            var result = _dbAccess.DeleteReviewByFoodItemId(999);

            // Assert
            Assert.That(result, Is.False);
        }
    }
}
