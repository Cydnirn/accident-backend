using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccidentBackend.Data;
using AccidentBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace AccidentBackend.Repository
{
    public class SiteRepository : Repository<Site>, ISiteRepository
    {
        public SiteRepository(AccidentDbContext context) : base(context)
        {
        }

        public async Task<Site?> GetBySiteCodeAsync(string siteCode)
        {
            return await _dbSet
                .FirstOrDefaultAsync(s => s.SiteCode == siteCode);
        }

        public async Task<IEnumerable<Site>> GetBySiteTypeAsync(string siteType)
        {
            return await _dbSet
                .Where(s => s.Type == siteType)
                .ToListAsync();
        }

        public async Task<Site?> GetSiteWithWorkersAsync(int id)
        {
            return await _dbSet
                .Where(s => s.Id == id)
                .Include(s => s.Workers).ThenInclude(w => w.Department)
                .FirstOrDefaultAsync();
        }

        public async Task<Site?> GetSiteWithAccidentsAsync(int id)
        {
            return await _dbSet
                .Where(s => s.Id == id)
                .Include(s => s.Accidents).ThenInclude(a => a.ReportedByWorker)
                .FirstOrDefaultAsync();
        }

        public async Task<Site?> GetSiteWithDetailsAsync(int id)
        {
            return await _dbSet
                .Where(s => s.Id == id)
                .Include(s => s.Workers)
                .Include(s => s.SafetyEquipments)
                .Include(s => s.Accidents)
                .FirstOrDefaultAsync();
        }
    }
}

