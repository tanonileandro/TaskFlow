using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace TaskFlow.Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IMapper _mapper;

        public AdminService(IAdminRepository adminRepository, IMapper mapper)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AdminDTO>> GetAllAsync()
        {
            var admins = await _adminRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AdminDTO>>(admins);
        }

        public async Task<AdminDTO> GetByIdAsync(int id)
        {
            var admin = await _adminRepository.GetByIdAsync(id);
            return _mapper.Map<AdminDTO>(admin);
        }

        public async Task<AdminDTO> GetByEmailAsync(string email)
        {
            var admin = await _adminRepository.GetByEmailAsync(email);
            return _mapper.Map<AdminDTO>(admin);
        }

        public async Task AddAsync(AdminDTO adminDto)
        {
            var admin = _mapper.Map<Admin>(adminDto);

            // Encriptar la contraseña antes de almacenarla
            admin.Password = BCrypt.Net.BCrypt.HashPassword(admin.Password);

            await _adminRepository.AddAsync(admin);
        }

        public async Task UpdateAsync(AdminDTO adminDto)
        {
            var admin = _mapper.Map<Admin>(adminDto);

            // Si estás actualizando la contraseña, encríptala
            if (!string.IsNullOrEmpty(adminDto.Password))
            {
                admin.Password = BCrypt.Net.BCrypt.HashPassword(adminDto.Password);
            }

            await _adminRepository.UpdateAsync(admin);
        }

        public async Task DeleteAsync(int id)
        {
            await _adminRepository.DeleteAsync(id);
        }
    }
}
