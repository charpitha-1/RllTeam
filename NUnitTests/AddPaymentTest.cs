using FoodieApp.DBAccess;
using FoodieApp.DTO;
using FoodieApp.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;

namespace FoodieNTest
{
    public class AddPaymentTests
    {
        private FoodieDbContext _context;
        private DbAccess _dbAccess;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<FoodieDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new FoodieDbContext(options);
            _dbAccess = new DbAccess(_context);

            // Seed required Order (foreign key dependency)
            _context.Orders.Add(new Order
            {
                OrderId = 1,
                OrderDate = DateTime.Now,
                OrderStatus = "Paid",
                UserId = 1,
                AddressId = 1,
                RestId = 1,
                OrderTotal = 200,
                FinalAmount = 200,
                gst = 0,
                Discount = 0,
                deliveredBy = "Test Rider",
                ScheduleDeliveryAt = DateTime.Now.AddHours(1)
            });

            _context.SaveChanges();
        }

        [TearDown]
        public void Cleanup() => _context.Dispose();

        [Test]
        public void AddPayment_ValidPayment_ReturnsTrueAndSavesPayment()
        {
            // Arrange
            var paymentDto = new PaymentDTO
            {
                PaymentMethod = "Credit Card",
                Amount = 200,
                PaymentStatus = "Completed",
                OrderId = 1
            };

            // Act
            var result = _dbAccess.AddPayment(paymentDto);

            // Assert
            Assert.That(result, Is.True);
            var savedPayment = _context.Payments.FirstOrDefault();
            Assert.That(savedPayment, Is.Not.Null);
            Assert.That(savedPayment.PaymentMethod, Is.EqualTo("Credit Card"));
            Assert.That(savedPayment.Amount, Is.EqualTo(200));
            Assert.That(savedPayment.PaymentStatus, Is.EqualTo("Completed"));
        }
    }
}
