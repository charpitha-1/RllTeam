using FoodieApp.DBAccess;
using FoodieApp.DTO;
using FoodieApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodieApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderLineItemController : ControllerBase
    {
        private readonly DbAccess dbAccess;

        public OrderLineItemController(DbAccess db)
        {
            dbAccess = db;
        }

        [HttpPost]
        public IActionResult AddOrderLineItem(OrderLineItemDTO data)
        {
            var oli = dbAccess.AddOrderLineItem(data);
            return Ok(new { result = "OrderLineItem added successfully", oli });
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderLineItem(int id)
        {
            var oli = dbAccess.GetOrderLineItemById(id);
            if (oli == null) return NotFound(new { result = false, message = "OrderLineItem not found" });
            return Ok(new { result = true, oli });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrderLineItem(int id, OrderLineItemDTO data)
        {
            if (id != data.OrderItemId)
                return BadRequest(new { result = false, message = "ID mismatch" });

            var updated = dbAccess.UpdateOrderLineItem(data);
            if (!updated) return NotFound(new { result = false, message = "OrderLineItem not found" });

            return Ok(new { result = true, message = "OrderLineItem updated successfully" });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrderLineItem(int id)
        {
            var deleted = dbAccess.DeleteOrderLineItem(id);
            if (!deleted) return NotFound(new { result = false, message = "OrderLineItem not found" });

            return Ok(new { result = true, message = "OrderLineItem deleted successfully" });
        }
    }
}