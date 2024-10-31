using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAdminRepository : IRepository<Admin>
    {
        Task<Admin> GetByEmailAsync(string email); // Método para buscar por email
    }
}
