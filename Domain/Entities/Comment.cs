using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public int CreatedById { get; set; }
        public User CreatedBy { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}