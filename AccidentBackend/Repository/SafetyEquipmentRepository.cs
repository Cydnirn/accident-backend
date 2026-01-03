using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccidentBackend.Data;
using AccidentBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace AccidentBackend.Repository
{
    public class SafetyEquipmentRepository : Repository<SafetyEquipment>, ISafetyEquipmentRepository
    {
        public SafetyEquipmentRepository(AccidentDbContext context) : base(context)
        {
        }

        public async Task<SafetyEquipment?> GetByTagNumberAsync(string tagNumber)
        {
            return await _dbSet
                .FirstOrDefaultAsync(e => e.TagNumber == tagNumber);
        }

        public async Task<IEnumerable<SafetyEquipment>> GetBySiteIdAsync(int siteId)
        {
            return await _dbSet
                .Where(e => e.SiteId == siteId)
                .Include(e => e.Site)
                .ToListAsync();
        }

        public async Task<IEnumerable<SafetyEquipment>> GetByStatusAsync(string status)
        {
            return await _dbSet
                .Where(e => e.Status == status)
                .Include(e => e.Site)
                .ToListAsync();
        }

        public async Task<IEnumerable<SafetyEquipment>> GetByEquipmentTypeAsync(string equipmentType)
        {
            return await _dbSet
                .Where(e => e.EquipmentType == equipmentType)
                .ToListAsync();
        }
    }
}

