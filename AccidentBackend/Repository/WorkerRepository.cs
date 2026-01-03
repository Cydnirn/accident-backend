using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccidentBackend.Data;
using AccidentBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace AccidentBackend.Repository
{
    public class WorkerRepository : Repository<Worker>, IWorkerRepository
    {
        public WorkerRepository(AccidentDbContext context) : base(context)
        {
        }

        public async Task<Worker?> GetByEmployeeNumberAsync(string EmployeeId)
        {
            return await _dbSet
                .FirstOrDefaultAsync(w => w.EmployeeId == EmployeeId);
        }

        public async Task<IEnumerable<Worker>> GetByDepartmentIdAsync(int departmentId)
        {
            return await _dbSet
                .Where(w => w.DepartmentId == departmentId)
                .Include(w => w.Department)
                .ToListAsync();
        }

        public async Task<IEnumerable<Worker>> GetBySiteIdAsync(int siteId)
        {
            return await _dbSet
                .Where(w => w.CurrentSiteId == siteId)
                .Include(w => w.CurrentSite)
                .Include(w => w.Department)
                .ToListAsync();
        }

        public async Task<IEnumerable<Worker>> GetContractorsAsync()
        {
            return await _dbSet
                .Where(w => w.IsContractor)
                .Include(w => w.CurrentSite)
                .ToListAsync();
        }

        public async Task<Worker?> GetWorkerWithDetailsAsync(int id)
        {
            return await _dbSet
                .Where(w => w.Id == id)
                .Include(w => w.Department)
                .Include(w => w.CurrentSite)
                .Include(w => w.ReportedAccidents)
                .Include(w => w.AccidentParticipations)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Worker>> GetWorkersWithAccidentsAsync()
        {
            return await _dbSet
                .Include(w => w.ReportedAccidents)
                .Include(w => w.AccidentParticipations)
                .Where(w => w.ReportedAccidents.Any() || w.AccidentParticipations.Any())
                .ToListAsync();
        }
    }
}

