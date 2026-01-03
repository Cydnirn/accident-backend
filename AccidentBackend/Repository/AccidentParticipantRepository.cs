using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccidentBackend.Data;
using AccidentBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace AccidentBackend.Repository
{
    public class AccidentParticipantRepository : Repository<AccidentParticipant>, IAccidentParticipantRepository
    {
        public AccidentParticipantRepository(AccidentDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<AccidentParticipant>> GetByAccidentIdAsync(long accidentId)
        {
            return await _dbSet
                .Where(ap => ap.AccidentId == accidentId)
                .Include(ap => ap.Worker)
                .ToListAsync();
        }

        public async Task<IEnumerable<AccidentParticipant>> GetByWorkerIdAsync(int workerId)
        {
            return await _dbSet
                .Where(ap => ap.WorkerId == workerId)
                .Include(ap => ap.Accident)
                .ToListAsync();
        }

        public async Task<IEnumerable<AccidentParticipant>> GetInjuredParticipantsAsync(long accidentId)
        {
            return await _dbSet
                .Where(ap => ap.AccidentId == accidentId && ap.Injured)
                .Include(ap => ap.Worker)
                .ToListAsync();
        }
    }
}

