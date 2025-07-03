using FoodieApp.DBAccess;
using FoodieApp.DTO;
using FoodieApp.Models;
using Microsoft.EntityFrameworkCore;
using System;

using NUnit.Framework;

namespace FoodieNTest
{
    public class DeleteOrderTest
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

            // Seed an order with line items
            var order = new Order
            {
                OrderId = 1,
                OrderDate = DateTime.Now,
                OrderStatus = "Pending",
                OrderTotal = 300,
                Discount = 0,
                gst = 0,
                FinalAmount = 300,
                UserId = 1,
                AddressId = 1,
                RestId = 1,
                ScheduleDeliveryAt = DateTime.Now.AddHours(1),
                deliveredBy = "Test Rider",
                orderLineItems = new List<OrderLineItem>
                {
                    new OrderLineItem
                    {
                        OrderItemId = 1,
                        Quantity = 2,
                        FoodItemId = 1
                    }
                }
            };

            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        [TearDown]
        public void Cleanup() => _context.Dispose();

        [Test]
        public void DeleteOrder_ExistingOrder_ReturnsTrueAndRemovesOrder()
        {
            // Act
            var result = _dbAccess.DeleteOrder(1);

            // Assert
            Assert.That(result, Is.True);
            Assert.That(_context.Orders.Count(), Is.EqualTo(0));
            Assert.That(_context.OrderLines.Count(), Is.EqualTo(0));
        }

        [Test]
        public void DeleteOrder_NonExistingOrder_ReturnsFalse()
        {
            // Act
            var result = _dbAccess.DeleteOrder(999);

            // Assert
           
            Assert.That(result, Is.False);
        }
    }
}
