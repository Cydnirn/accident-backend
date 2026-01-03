using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccidentBackend.Data;
using AccidentBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace AccidentBackend.Repository
{
    public class AttachmentRepository : Repository<Attachment>, IAttachmentRepository
    {
        public AttachmentRepository(AccidentDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Attachment>> GetByAccidentIdAsync(long accidentId)
        {
            return await _dbSet
                .Where(a => a.AccidentId == accidentId)
                .Include(a => a.UploadedByWorker)
                .OrderBy(a => a.UploadedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Attachment>> GetByWorkerIdAsync(int workerId)
        {
            return await _dbSet
                .Where(a => a.UploadedByWorkerId == workerId)
                .Include(a => a.Accident)
                .ToListAsync();
        }

        public async Task<IEnumerable<Attachment>> GetByContentTypeAsync(string contentType)
        {
            return await _dbSet
                .Where(a => a.ContentType == contentType)
                .ToListAsync();
        }
    }
}

