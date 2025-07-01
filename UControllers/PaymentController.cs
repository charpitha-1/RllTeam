using FoodieApp.DBAccess;
using FoodieApp.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodieApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly DbAccess dbAccess;

        public PaymentController(DbAccess db)
        {
            dbAccess = db;
        }

        [HttpPost]
        public IActionResult AddPayment(PaymentDTO data)
        {
            if (data == null)
                return BadRequest(new { result = false, message = "Invalid payment data" });

            bool isAdded = dbAccess.AddPayment(data);
            if (!isAdded)
                return BadRequest(new { result = false, message = "Payment creation failed" });

            return Ok(new { result = true, message = "Payment added successfully" });
        }

        [HttpGet("{orderId}")]
        public IActionResult GetPaymentByOrderId(int orderId)
        {
            var payment = dbAccess.GetPaymentByOrderId(orderId);
            if (payment == null)
                return NotFound(new { result = false, message = "Payment not found for this order" });

            return Ok(new { result = true, data = payment });
        }
        [HttpGet]
        public IActionResult GetAllPayments()
        {
            var payments = dbAccess.GetAllPayments();
            if (payments == null || payments.Count == 0)
                return NotFound(new { result = false, message = "No payments found" });

            return Ok(new { result = true, data = payments });
        }


        [HttpPut("{orderId}")]
        public IActionResult UpdatePaymentByOrderId(int orderId, PaymentDTO data)
        {
            if (orderId != data.OrderId)
                return BadRequest(new { result = false, message = "OrderId mismatch" });

            bool isUpdated = dbAccess.UpdatePaymentByOrderId(data);
            if (!isUpdated)
                return NotFound(new { result = false, message = "Payment not found or update failed" });

            return Ok(new { result = true, message = "Payment updated successfully" });
        }


        [HttpDelete("{orderId}")]
        public IActionResult DeletePaymentByOrderId(int orderId)
        {
            bool isDeleted = dbAccess.DeletePaymentByOrderId(orderId);
            if (!isDeleted)
                return NotFound(new { result = false, message = "Payment not found or already deleted" });

            return Ok(new { result = true, message = "Payment deleted successfully" });
        }



    }
}

