using CoworkingBooking.Service.DTOs;
using CoworkingBooking.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoworkingBooking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] int pageIndex, int pageSize)
        {
            return Ok(await userService.GetAllAsync(pageIndex, pageSize));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(long id)
        {
            return Ok(await userService.GetAsync(u => u.Id == id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(long id, UserDTO userDto)
        {
            return Ok(await userService.UpdateAsync(id, userDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            return Ok(userService.DeleteAsync(id));
        }
    }
}
