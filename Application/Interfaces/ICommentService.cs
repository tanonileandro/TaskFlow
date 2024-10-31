using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentDTO>> GetAllAsync();
        Task<CommentDTO> GetByIdAsync(int id);
        Task AddAsync(CommentDTO commentDto);
        Task UpdateAsync(CommentDTO commentDto);
        Task DeleteAsync(int id);
    }
}