
using AccidentBackend.Models;
using System.Threading.Tasks;

namespace AccidentBackend.Repository
{
    public interface IWitnessRepository : IRepository<Witness>
    {
        Task<IEnumerable<Witness>> GetByWorkerIdAsync(int workerId);
        Task<IEnumerable<Witness>> GetByAccidentIdAsync(long accidentId);
    }
}


