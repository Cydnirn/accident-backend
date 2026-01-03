using AccidentBackend.Models;
namespace AccidentBackend.Repository
{
    public interface IAccidentRepository : IRepository<Accident>
    {
        Task<IEnumerable<Accident>> GetAccidentsWithDetailsAsync();
        Task<Accident?> GetAccidentWithDetailsAsync(long id);
        Task<IEnumerable<Accident>> GetFatalAccidentsAsync();
        Task<Accident?> GetByAccidentNumberAsync(string accidentNumber);
        Task<IEnumerable<Accident>> GetByStatusAsync(string status);
        Task<IEnumerable<Accident>> GetBySeverityLevelAsync(short severityLevel);
        Task<IEnumerable<Accident>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<Accident>> GetBySiteIdAsync(int siteId);
    }
}


