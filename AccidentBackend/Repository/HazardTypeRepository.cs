using System.Threading.Tasks;
using AccidentBackend.Data;
using AccidentBackend.Interface;
using AccidentBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace AccidentBackend.Repository
{
    public class HazardTypeRepository : Repository<HazardType>, IHazardTypeRepository
    {
        public HazardTypeRepository(AccidentDbContext context) : base(context)
        {
        }

        public async Task<HazardType?> GetByCodeAsync(string code)
        {
            return await _dbSet
                .FirstOrDefaultAsync(h => h.Code == code);
        }
    }
}

