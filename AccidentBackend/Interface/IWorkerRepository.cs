using System.Collections.Generic;
using System.Threading.Tasks;
using AccidentBackend.Models;

namespace AccidentBackend.Repository
{
    public interface IWorkerRepository : IRepository<Worker>
    {
        Task<Worker?> GetByEmployeeNumberAsync(string employeeNumber);
        Task<IEnumerable<Worker>> GetByDepartmentIdAsync(int departmentId);
        Task<IEnumerable<Worker>> GetBySiteIdAsync(int siteId);
        Task<IEnumerable<Worker>> GetContractorsAsync();
        Task<Worker?> GetWorkerWithDetailsAsync(int id);
        Task<IEnumerable<Worker>> GetWorkersWithAccidentsAsync();
    }
}

