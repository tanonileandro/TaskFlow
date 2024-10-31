using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly IRepository<Comment> _commentRepository;
        private readonly IMapper _mapper;

        public CommentService(IRepository<Comment> commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CommentDTO>> GetAllAsync()
        {
            var comments = await _commentRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CommentDTO>>(comments);
        }

        public async Task<CommentDTO> GetByIdAsync(int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            return _mapper.Map<CommentDTO>(comment);
        }

        public async Task AddAsync(CommentDTO commentDto)
        {
            var comment = _mapper.Map<Comment>(commentDto);
            await _commentRepository.AddAsync(comment);
        }

        public async Task UpdateAsync(CommentDTO commentDto)
        {
            var comment = _mapper.Map<Comment>(commentDto);
            await _commentRepository.UpdateAsync(comment);
        }

        public async Task DeleteAsync(int id)
        {
            await _commentRepository.DeleteAsync(id);
        }
    }
}
