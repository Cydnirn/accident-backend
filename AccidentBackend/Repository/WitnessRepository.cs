using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccidentBackend.Data;
using AccidentBackend.Interface;
using AccidentBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace AccidentBackend.Repository
{
    public class WitnessRepository : Repository<Witness>, IWitnessRepository
    {
        public WitnessRepository(AccidentDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Witness>> GetByAccidentIdAsync(long accidentId)
        {
            return await _dbSet
                .Where(w => w.AccidentId == accidentId)
                .Include(w => w.Worker)
                .ToListAsync();
        }

        public async Task<IEnumerable<Witness>> GetByWorkerIdAsync(int workerId)
        {
            return await _dbSet
                .Where(w => w.WorkerId == workerId)
                .Include(w => w.Accident)
                .ToListAsync();
        }
    }
}

