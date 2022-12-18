using CoworkingBooking.Service.DTOs;
using CoworkingBooking.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoworkingBooking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TablesController : ControllerBase
    {
        private readonly ITableService tableService;

        public TablesController(ITableService tableService)
        {
            this.tableService = tableService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] int pageIndex, int pageSize)
        {
            return Ok(await tableService.GetAllAsync(pageIndex, pageSize));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(long id)
        {
            return Ok(await tableService.GetAsync(b => b.Id == id));
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAsync(TableDTO tableDto)
        {
            return Ok(await tableService.CreateAsync(tableDto));
        }

        [HttpPut("{id}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAsync(long id, TableDTO tableDto)
        {
            return Ok(await tableService.UpdateAsync(id, tableDto));
        }

        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            return Ok(tableService.DeleteAsync(id));
        }
    }
}
