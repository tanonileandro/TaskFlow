using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace TaskFlow.Infrastructure.Repositories
{
    public class AdminRepository : Repository<Admin>, IAdminRepository
    {
        public AdminRepository(TaskFlowDbContext context) : base(context)
        {
        }

        public async Task<Admin> GetByEmailAsync(string email)
        {
            return await _context.Admins.FirstOrDefaultAsync(a => a.Email == email);
        }
    }
}
