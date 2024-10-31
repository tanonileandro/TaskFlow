using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace TaskFlow.Infrastructure.Repositories
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(TaskFlowDbContext context) : base(context)
        {
        }

        public async Task<Client> GetByEmailAsync(string email)
        {
            return await _context.Clients.FirstOrDefaultAsync(c => c.Email == email);
        }
    }
}