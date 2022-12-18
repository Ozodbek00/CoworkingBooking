using CoworkingBooking.Service.DTOs;
using CoworkingBooking.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoworkingBooking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChairsController : ControllerBase
    {
        private readonly IChairService chairService;

        public ChairsController(IChairService chairService)
        {
            this.chairService = chairService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] int pageIndex, int pageSize)
        {
            return Ok(await chairService.GetAllAsync(pageIndex, pageSize));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(long id)
        {
            return Ok(await chairService.GetAsync(b => b.Id == id));
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAsync(ChairDTO chairDto)
        {
            return Ok(await chairService.CreateAsync(chairDto));
        }

        [HttpPut("{id}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAsync(long id, ChairDTO chairDto)
        {
            return Ok(await chairService.UpdateAsync(id, chairDto));
        }

        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            return Ok(chairService.DeleteAsync(id));
        }
    }
}
