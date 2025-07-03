using FoodieApp.DBAccess;
using FoodieApp.DTO;
using FoodieApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static FoodieApp.DTO.OrderDetailsDTO;

namespace FoodieApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly DbAccess dbAccess;

        public OrderDetailsController(DbAccess db)
        {
            dbAccess = db;

        }
        [HttpGet("{orderId}")]
        public IActionResult GetOrder(int orderId)
        {
            var order = dbAccess.GetOrder(orderId);
            if (order == null) return NotFound();
            return Ok(order);
        }


        [HttpGet]
        public IActionResult GetAllOrders()
        {
            var allOrders = dbAccess.GetAllOrders();
            if (allOrders == null || !allOrders.Any())
                return NotFound("No orders found.");

            return Ok(allOrders);
        }


        [HttpPost]
        public IActionResult CreateOrder(OrderDetailsDTO orderDto)
        {
            if (orderDto == null || orderDto.OrderLineItems == null || !orderDto.OrderLineItems.Any())
            {
                return BadRequest(new { result = false, message = "Invalid or incomplete order data" });
            }

            var createdOrder = dbAccess.AddOrder(orderDto);

            if (createdOrder == null)
            {
                return BadRequest(new { result = false, message = "Failed to create order" });
            }

            return CreatedAtAction(nameof(GetOrder), new { orderId = createdOrder.OrderId }, new
            {
                result = true,
                message = "Order created successfully",
                data = createdOrder
            });
        }


        [HttpPut("{orderId}")]
        public IActionResult UpdateOrder(int orderId, OrderDetailsDTO orderDto)
        {
            if (orderId != orderDto.OrderId)
                return BadRequest(new { result = false, message = "OrderId mismatch" });

            bool isUpdated = dbAccess.UpdateOrder(orderDto);
            if (!isUpdated)
                return NotFound(new { result = false, message = "Order not found or update failed" });

            return Ok(new { result = true, message = "Order updated successfully" });
        }


        [HttpDelete("{orderId}")]
        public IActionResult DeleteOrder(int orderId)
        {
            bool isDeleted = dbAccess.DeleteOrder(orderId);

            if (!isDeleted)
                return NotFound(new { result = false, message = "Order not found or already deleted" });

            return Ok(new { result = true, message = "Order deleted successfully" });
        }
        //================================================Order Using UserId


        [HttpGet("User/{userId}")]
        public ActionResult<List<OrderDetailsDTO>> GetOrdersByUserId(int userId)
        {
            var orders = dbAccess.GetOrdersByUserId(userId);

            if (orders == null || orders.Count == 0)
            {
                return NotFound($"No orders found for user with ID {userId}.");
            }

            return Ok(orders);
        }





    }



}

