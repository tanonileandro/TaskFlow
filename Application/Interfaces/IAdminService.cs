using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IAdminService
    {
        Task<IEnumerable<AdminDTO>> GetAllAsync();
        Task<AdminDTO> GetByIdAsync(int id);
        Task<AdminDTO> GetByEmailAsync(string email);
        Task AddAsync(AdminDTO adminDto);
        Task UpdateAsync(AdminDTO adminDto);
        Task DeleteAsync(int id);
    }
}
