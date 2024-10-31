using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDTO>> GetAllAsync();
        Task<ProjectDTO> GetByIdAsync(int id);
        Task AddAsync(ProjectDTO projectDto);
        Task UpdateAsync(ProjectDTO projectDto);
        Task DeleteAsync(int id);
    }
}
