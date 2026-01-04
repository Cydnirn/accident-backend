using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccidentBackend.Data;
using AccidentBackend.Interface;
using AccidentBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace AccidentBackend.Repository
{
    public class ActionTakenRepository : Repository<ActionTaken>, IActionTakenRepository
    {
        public ActionTakenRepository(AccidentDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ActionTaken>> GetByAccidentIdAsync(long accidentId)
        {
            return await _dbSet
                .Where(at => at.AccidentId == accidentId)
                .Include(at => at.PerformedByWorker)
                .OrderBy(at => at.ActionTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<ActionTaken>> GetByWorkerIdAsync(int workerId)
        {
            return await _dbSet
                .Where(at => at.PerformedByWorkerId == workerId)
                .Include(at => at.Accident)
                .ToListAsync();
        }

        public async Task<IEnumerable<ActionTaken>> GetByActionTypeAsync(string actionType)
        {
            return await _dbSet
                .Where(at => at.ActionType == actionType)
                .Include(at => at.PerformedByWorker)
                .ToListAsync();
        }
    }
}

