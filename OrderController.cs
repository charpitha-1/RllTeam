using FoodieApp.DBAccess;
using FoodieApp.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodieApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly DbAccess dbAccess;

        public OrderController(DbAccess db)
        {
            dbAccess = db;
        }

        [HttpPost]
        public IActionResult AddOrder(OrderDTO data)
        {
            var order = dbAccess.AddOrder(data);
            return Ok(new { result = "Order added successfully", order });
        }

        [HttpGet("{id}")]
        public IActionResult GetOrder(int id)
        {
            var order = dbAccess.GetOrderById(id);
            if (order == null) return NotFound(new { result = false, message = "Order not found" });
            return Ok(new { result = true, order });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, OrderDTO data)
        {
            if (id != data.OrderId)
                return BadRequest(new { result = false, message = "ID mismatch" });

            var updated = dbAccess.UpdateOrder(data);
            if (!updated) return NotFound(new { result = false, message = "Order not found" });

            return Ok(new { result = true, message = "Order updated successfully" });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var deleted = dbAccess.DeleteOrder(id);
            if (!deleted) return NotFound(new { result = false, message = "Order not found" });

            return Ok(new { result = true, message = "Order deleted successfully" });
        }
    }
}

