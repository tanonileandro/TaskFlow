using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminsController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminsController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        // GET: api/Admins
        [HttpGet]
        public async Task<IActionResult> GetAdmins()
        {
            var admins = await _adminService.GetAllAsync();
            return Ok(admins);
        }

        // GET: api/Admins/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdmin(int id)
        {
            var admin = await _adminService.GetByIdAsync(id);
            if (admin == null)
                return NotFound();

            return Ok(admin);
        }

        // POST: api/Admins
        [HttpPost]
        public async Task<IActionResult> PostAdmin([FromBody] AdminDTO adminDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _adminService.AddAsync(adminDto);
            return CreatedAtAction(nameof(GetAdmin), new { id = adminDto.Id }, adminDto);
        }

        // PUT: api/Admins/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdmin(int id, [FromBody] AdminDTO adminDto)
        {
            if (id != adminDto.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _adminService.UpdateAsync(adminDto);
            return NoContent();
        }

        // DELETE: api/Admins/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            await _adminService.DeleteAsync(id);
            return NoContent();
        }
    }
}