using AccidentBackend.Models;
using System.Threading.Tasks;

namespace AccidentBackend.Interface
{

    public interface IAccidentParticipantRepository : IRepository<AccidentParticipant>
    {
        Task<IEnumerable<AccidentParticipant>> GetInjuredParticipantsAsync(long accidentId);
        Task<IEnumerable<AccidentParticipant>> GetByWorkerIdAsync(int workerId);
        Task<IEnumerable<AccidentParticipant>> GetByAccidentIdAsync(long accidentId);
    }
}


