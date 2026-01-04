using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccidentBackend.Data;
using AccidentBackend.Interface;
using AccidentBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace AccidentBackend.Repository
{
    public class AccidentRepository : Repository<Accident>, IAccidentRepository
    {
        public AccidentRepository(AccidentDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Accident>> GetBySiteIdAsync(int siteId)
        {
            return await _dbSet
                .Where(a => a.SiteId == siteId)
                .Include(a => a.Site)
                .Include(a => a.ReportedByWorker)
                .ToListAsync();
        }

        public async Task<IEnumerable<Accident>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbSet
                .Where(a => a.OccurredAt >= startDate && a.OccurredAt <= endDate)
                .Include(a => a.Site)
                .OrderByDescending(a => a.OccurredAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Accident>> GetBySeverityLevelAsync(short severityLevel)
        {
            return await _dbSet
                .Where(a => a.SeverityLevel == severityLevel)
                .Include(a => a.Site)
                .ToListAsync();
        }

        public async Task<IEnumerable<Accident>> GetByStatusAsync(string status)
        {
            return await _dbSet
                .Where(a => a.Status == status)
                .Include(a => a.Site)
                .ToListAsync();
        }

        public async Task<Accident?> GetByAccidentNumberAsync(string accidentNumber)
        {
            return await _dbSet
                .FirstOrDefaultAsync(a => a.AccidentNumber == accidentNumber);
        }

        public async Task<IEnumerable<Accident>> GetFatalAccidentsAsync()
        {
            return await _dbSet
                .Where(a => a.IsFatal)
                .Include(a => a.Site)
                .Include(a => a.ReportedByWorker)
                .OrderByDescending(a => a.OccurredAt)
                .ToListAsync();
        }

        public async Task<Accident?> GetAccidentWithDetailsAsync(long id)
        {
            return await _dbSet
                .Where(a => a.Id == id)
                .Include(a => a.Site)
                .Include(a => a.ReportedByWorker)
                .Include(a => a.Shift)
                .Include(a => a.HazardType)
                .Include(a => a.Cause)
                .Include(a => a.Participants).ThenInclude(p => p.Worker)
                .Include(a => a.AccidentEquipments).ThenInclude(ae => ae.Equipment)
                .Include(a => a.Witnesses).ThenInclude(w => w.Worker)
                .Include(a => a.ActionsTaken).ThenInclude(at => at.PerformedByWorker)
                .Include(a => a.Attachments).ThenInclude(att => att.UploadedByWorker)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Accident>> GetAccidentsWithDetailsAsync()
        {
            return await _dbSet
                .Include(a => a.Site)
                .Include(a => a.ReportedByWorker)
                .Include(a => a.Shift)
                .Include(a => a.HazardType)
                .Include(a => a.Cause)
                .Include(a => a.Participants)
                .OrderByDescending(a => a.OccurredAt)
                .ToListAsync();
        }
    }
}

