using CoworkingBooking.Service.DTOs;
using CoworkingBooking.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoworkingBooking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FloorsController : ControllerBase
    {
        private readonly IFloorService floorService;

        public FloorsController(IFloorService floorService)
        {
            this.floorService = floorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] int pageIndex, int pageSize)
        {
            return Ok(await floorService.GetAllAsync(pageIndex, pageSize));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(long id)
        {
            return Ok(await floorService.GetAsync(b => b.Id == id));
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAsync(FloorDTO floorDto)
        {
            return Ok(await floorService.CreateAsync(floorDto));
        }

        [HttpPut("{id}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAsync(long id, FloorDTO floorDto)
        {
            return Ok(await floorService.UpdateAsync(id, floorDto));
        }

        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            return Ok(floorService.DeleteAsync(id));
        }
    }
}
