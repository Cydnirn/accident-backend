using System.Collections.Generic;
using System.Threading.Tasks;
using AccidentBackend.Models;

namespace AccidentBackend.Repository
{
    public interface IActionTakenRepository : IRepository<ActionTaken>
    {
        Task<IEnumerable<ActionTaken>> GetByAccidentIdAsync(long accidentId);
        Task<IEnumerable<ActionTaken>> GetByWorkerIdAsync(int workerId);
        Task<IEnumerable<ActionTaken>> GetByActionTypeAsync(string actionType);
    }
}

