using CoworkingBooking.Service.DTOs;
using CoworkingBooking.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoworkingBooking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrdersController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet, Authorize(Roles = "User")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAsync([FromQuery] int pageIndex, int pageSize)
        {
            return Ok(await orderService.GetAllAsync(pageIndex, pageSize));
        }

        [HttpGet("{id}"), Authorize(Roles = "User")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAsync(long id)
        {
            return Ok(await orderService.GetAsync(b => b.Id == id));
        }

        [HttpPost, Authorize(Roles = "User")]
        public async Task<IActionResult> CreateAsync(OrderDTO orderDto)
        {
            return Ok(await orderService.CreateAsync(orderDto));
        }

        [HttpPut("{id}"), Authorize(Roles = "Admin")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateAsync(long id, OrderDTO orderDto)
        {
            return Ok(await orderService.UpdateAsync(id, orderDto));
        }

        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            return Ok(orderService.DeleteAsync(id));
        }
    }
}
