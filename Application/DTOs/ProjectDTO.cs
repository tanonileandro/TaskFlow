using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ProjectDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del proyecto es obligatorio.")]
        public string ProjectName { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "El ID del administrador es obligatorio.")]
        public int AdminId { get; set; }

        [Required(ErrorMessage = "El ID del cliente es obligatorio.")]
        public int ClientId { get; set; }

        public bool State { get; set; } = true;
    }
}
