using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Client : User
    {
        public List<Project> AssignedProjects { get; set; } = new List<Project>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}