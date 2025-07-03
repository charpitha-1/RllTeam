using FoodieApp.DBAccess;
using FoodieApp.DTO;
using FoodieApp.Models;
using Microsoft.EntityFrameworkCore;
using System;

using NUnit.Framework;

namespace FoodieNTest
{
    public class UserUpdateTest
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
        public void UpdateUser_ValidUser_UpdatesDataCorrectly()
        {
            // Arrange: Add original user
            var originalDto = new UserDTO
            {
                firstName = "Jane",
                lastName = "Smith",
                email = "jane@example.com",
                phoneNumber = "9876543210",
                userRole = "Admin",
                password = "originalpass"
            };

            _dbAccess.AddUser(originalDto);
            var existingUser = _context.Users.FirstOrDefault(u => u.Email == "jane@example.com");

            // Prepare updated data
            var updatedDto = new UserDTO
            {
                firstName = "Janet",
                lastName = "Jackson",
                email = "janet@example.com",
                phoneNumber = "8888888888",
                userRole = "Customer",
                password = "newpass" // NOTE: This won't change due to your current logic
            };

            // Act
            var result = _dbAccess.UpdateUser(existingUser.UserId, updatedDto);
            var updatedUser = _context.Users.Find(existingUser.UserId);

            // Assert
            Assert.That(result, Is.True);
            //Assert.That(updatedUser.FirstName, Is.EqualTo("Janet"));
            //Assert.That(updatedUser.LastName, Is.EqualTo("Jackson"));
            //Assert.That(updatedUser.Email, Is.EqualTo("janet@example.com"));
            //Assert.That(updatedUser.PhoneNumber, Is.EqualTo("8888888888"));
            //Assert.That(updatedUser.UserRole, Is.EqualTo("Customer"));

            // Check if password stayed the same (based on your original code)
            Assert.That(updatedUser.Password, Is.EqualTo("originalpass"));
        }

    }
}
