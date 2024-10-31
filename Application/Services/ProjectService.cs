using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace TaskFlow.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IRepository<Project> _projectRepository;
        private readonly IMapper _mapper;

        public ProjectService(IRepository<Project> projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProjectDTO>> GetAllAsync()
        {
            var projects = await _projectRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProjectDTO>>(projects);
        }

        public async Task<ProjectDTO> GetByIdAsync(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            return _mapper.Map<ProjectDTO>(project);
        }

        public async Task AddAsync(ProjectDTO projectDto)
        {
            var project = _mapper.Map<Project>(projectDto);
            await _projectRepository.AddAsync(project);
        }

        public async Task UpdateAsync(ProjectDTO projectDto)
        {
            var project = _mapper.Map<Project>(projectDto);
            await _projectRepository.UpdateAsync(project);
        }

        public async Task DeleteAsync(int id)
        {
            await _projectRepository.DeleteAsync(id);
        }
    }
}
