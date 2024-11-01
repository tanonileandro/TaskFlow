using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            var projects = await _projectService.GetAllAsync();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProject(int id)
        {
            var project = await _projectService.GetByIdAsync(id);
            if (project == null)
                return NotFound();

            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> PostProject([FromBody] ProjectDTO projectDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _projectService.AddAsync(projectDto);
            return CreatedAtAction(nameof(GetProject), new { id = projectDto.Id }, projectDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(int id, [FromBody] ProjectDTO projectDto)
        {
            if (id != projectDto.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _projectService.UpdateAsync(projectDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            await _projectService.DeleteAsync(id);
            return NoContent();
        }
    }
}
