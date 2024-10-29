using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Admin : User
    {
        public List<Project> CreatedProjects { get; set; } = new List<Project>();
        public List<Comment> CreatedComments { get; set; } = new List<Comment>();
    }
}
