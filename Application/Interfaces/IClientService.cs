using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IClientService
    {
        Task<IEnumerable<ClientDTO>> GetAllAsync();
        Task<ClientDTO> GetByIdAsync(int id);
        Task<ClientDTO> GetByEmailAsync(string email);
        Task AddAsync(ClientDTO clientDto);
        Task UpdateAsync(ClientDTO clientDto);
        Task DeleteAsync(int id);
    }
}
