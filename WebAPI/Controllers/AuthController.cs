using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IAdminService _adminService;
        private readonly IClientService _clientService;

        public AuthController(IAuthService authService, IAdminService adminService, IClientService clientService)
        {
            _authService = authService;
            _adminService = adminService;
            _clientService = clientService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            var token = await _authService.Authenticate(loginDto.Email, loginDto.Password);

            if (token == null)
                return Unauthorized(new { message = "Email o contraseña incorrectos." });

            return Ok(new { token });
        }

        [HttpPost("register/admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] AdminDTO adminDto)
        {
            await _adminService.AddAsync(adminDto);
            return Ok(new { message = "Administrador registrado exitosamente." });
        }

        [HttpPost("register/client")]
        public async Task<IActionResult> RegisterClient([FromBody] ClientDTO clientDto)
        {
            await _clientService.AddAsync(clientDto);
            return Ok(new { message = "Cliente registrado exitosamente." });
        }
    }
}