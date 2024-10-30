using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CommentDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El contenido del comentario es obligatorio.")]
        public string Content { get; set; }

        [Required(ErrorMessage = "El ID del creador es obligatorio.")]
        public int CreatedById { get; set; }

        [Required(ErrorMessage = "El ID del proyecto es obligatorio.")]
        public int ProjectId { get; set; }
    }
}
