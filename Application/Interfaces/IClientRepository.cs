using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IClientRepository : IRepository<Client>
    {
        Task<Client> GetByEmailAsync(string email); // Método para buscar por email
    }
}
