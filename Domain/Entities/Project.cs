using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public bool State { get; set; } = true;

        // Relación con Admin (quien creó el proyecto)
        public int AdminId { get; set; }
        public Admin Admin { get; set; }

        // Relación con Client (cliente asignado al proyecto)
        public int ClientId { get; set; }
        public Client Client { get; set; }

        // Comentarios asociados al proyecto
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
