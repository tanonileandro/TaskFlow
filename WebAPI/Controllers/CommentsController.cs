using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        // GET: api/Comments
        [HttpGet]
        public async Task<IActionResult> GetComments()
        {
            var comments = await _commentService.GetAllAsync();
            return Ok(comments);
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetComment(int id)
        {
            var comment = await _commentService.GetByIdAsync(id);
            if (comment == null)
                return NotFound();

            return Ok(comment);
        }

        // POST: api/Comments
        [HttpPost]
        public async Task<IActionResult> PostComment([FromBody] CommentDTO commentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _commentService.AddAsync(commentDto);
            return CreatedAtAction(nameof(GetComment), new { id = commentDto.Id }, commentDto);
        }

        // PUT: api/Comments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(int id, [FromBody] CommentDTO commentDto)
        {
            if (id != commentDto.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _commentService.UpdateAsync(commentDto);
            return NoContent();
        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            await _commentService.DeleteAsync(id);
            return NoContent();
        }
    }
}