using Application.Interfaces;
using Application.Models;
using BCrypt.Net;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IClientRepository _clientRepository;
        private readonly JwtSettings _jwtSettings;

        public AuthService(IAdminRepository adminRepository, IClientRepository clientRepository, IOptions<JwtSettings> jwtSettings)
        {
            _adminRepository = adminRepository;
            _clientRepository = clientRepository;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<string> Authenticate(string email, string password)
        {
            User user = await _adminRepository.GetByEmailAsync(email) ?? (User)await _clientRepository.GetByEmailAsync(email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            var keyBytes = Encoding.UTF8.GetBytes(_jwtSettings.Key);

            if (keyBytes.Length < 32)
            {
                Array.Resize(ref keyBytes, 32);
                for (int i = _jwtSettings.Key.Length, j = 0; i < 32; i++, j++)
                {
                    keyBytes[i] = keyBytes[j % _jwtSettings.Key.Length];
                }
            }

            var key = new SymmetricSecurityKey(keyBytes);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user is Admin ? "Admin" : "Client")
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                Issuer = _jwtSettings.Issuer,
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
