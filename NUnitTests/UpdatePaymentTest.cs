using FoodieApp.DBAccess;
using FoodieApp.DTO;
using FoodieApp.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;

namespace FoodieNTest
{
    public class UpdatePaymentTest
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

            // Seed order and initial payment record
            _context.Orders.Add(new Order
            {
                OrderId = 1,
                OrderDate = DateTime.Now,
                OrderStatus = "Pending",
                UserId = 1,
                AddressId = 1,
                RestId = 1,
                OrderTotal = 300,
                Discount = 0,
                gst = 0,
                FinalAmount = 300,
                deliveredBy = "Rider",
                ScheduleDeliveryAt = DateTime.Now.AddHours(1)
            });

            _context.Payments.Add(new Payment
            {
                PaymentId = 1,
                OrderId = 1,
                PaymentMethod = "UPI",
                Amount = 300,
                PaymentStatus = "Pending"
            });

            _context.SaveChanges();
        }

        [TearDown]
        public void Cleanup() => _context.Dispose();

        [Test]
        public void UpdatePayment_ExistingOrderId_ReturnsTrueAndUpdatesPayment()
        {
            // Arrange
            var updatedPayment = new PaymentDTO
            {
                OrderId = 1,
                PaymentMethod = "Credit Card",
                Amount = 300,
                PaymentStatus = "Completed"
            };

            // Act
            var result = _dbAccess.UpdatePaymentByOrderId(updatedPayment);

            // Assert
            Assert.That(result, Is.True);
            var payment = _context.Payments.FirstOrDefault(p => p.OrderId == 1);
            Assert.That(payment, Is.Not.Null);
            Assert.That(payment.PaymentMethod, Is.EqualTo("Credit Card"));
            Assert.That(payment.PaymentStatus, Is.EqualTo("Completed"));
        }

        [Test]
        public void UpdatePayment_NonExistingOrderId_ReturnsFalse()
        {
            // Arrange
            var invalidPayment = new PaymentDTO
            {
                OrderId = 99,
                PaymentMethod = "Cash",
                Amount = 100,
                PaymentStatus = "Failed"
            };

            // Act
            var result = _dbAccess.UpdatePaymentByOrderId(invalidPayment);

            // Assert
            Assert.That(result, Is.False);
        }
    }
}
