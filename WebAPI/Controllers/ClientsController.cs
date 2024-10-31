using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        // GET: api/Clients
        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            var clients = await _clientService.GetAllAsync();
            return Ok(clients);
        }

        // GET: api/Clients/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClient(int id)
        {
            var client = await _clientService.GetByIdAsync(id);
            if (client == null)
                return NotFound();

            return Ok(client);
        }

        // POST: api/Clients
        [HttpPost]
        public async Task<IActionResult> PostClient([FromBody] ClientDTO clientDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _clientService.AddAsync(clientDto);
            return CreatedAtAction(nameof(GetClient), new { id = clientDto.Id }, clientDto);
        }

        // PUT: api/Clients/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(int id, [FromBody] ClientDTO clientDto)
        {
            if (id != clientDto.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _clientService.UpdateAsync(clientDto);
            return NoContent();
        }

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            await _clientService.DeleteAsync(id);
            return NoContent();
        }
    }
}
