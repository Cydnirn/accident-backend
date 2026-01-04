using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccidentBackend.Data;
using AccidentBackend.Interface;
using AccidentBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace AccidentBackend.Repository
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(AccidentDbContext context) : base(context)
        {
        }

        public async Task<Department?> GetDepartmentWithWorkersAsync(int id)
        {
            return await _dbSet
                .Where(d => d.Id == id)
                .Include(d => d.Workers)
                .Include(d => d.ManagerWorker)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Department>> GetDepartmentsWithWorkersAsync()
        {
            return await _dbSet
                .Include(d => d.Workers)
                .Include(d => d.ManagerWorker)
                .ToListAsync();
        }
    }
}

