using System.Threading.Tasks;
using AccidentBackend.Data;
using AccidentBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace AccidentBackend.Repository
{
    public class AccidentCauseRepository : Repository<AccidentCause>, IAccidentCauseRepository
    {
        public AccidentCauseRepository(AccidentDbContext context) : base(context)
        {
        }

        public async Task<AccidentCause?> GetByCodeAsync(string code)
        {
            return await _dbSet
                .FirstOrDefaultAsync(c => c.Code == code);
        }
    }
}

