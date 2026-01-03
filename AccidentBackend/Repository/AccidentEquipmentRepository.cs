using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccidentBackend.Data;
using AccidentBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace AccidentBackend.Repository
{
    public class AccidentEquipmentRepository : Repository<AccidentEquipment>, IAccidentEquipmentRepository
    {
        public AccidentEquipmentRepository(AccidentDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<AccidentEquipment>> GetByAccidentIdAsync(long accidentId)
        {
            return await _dbSet
                .Where(ae => ae.AccidentId == accidentId)
                .Include(ae => ae.Equipment)
                .ToListAsync();
        }

        public async Task<IEnumerable<AccidentEquipment>> GetByEquipmentIdAsync(int equipmentId)
        {
            return await _dbSet
                .Where(ae => ae.EquipmentId == equipmentId)
                .Include(ae => ae.Accident)
                .ToListAsync();
        }
    }
}

