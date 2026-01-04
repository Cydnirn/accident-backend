using System.Threading.Tasks;
using AccidentBackend.Models;

namespace AccidentBackend.Interface
{
    public interface IAccidentCauseRepository : IRepository<AccidentCause>
    {
        Task<AccidentCause?> GetByCodeAsync(string code);
    }
}

