using AccidentBackend.Models;
using System.Threading.Tasks;

namespace AccidentBackend.Repository
{
    public interface IAttachmentRepository : IRepository<Attachment>
    {
        Task<IEnumerable<Attachment>> GetByContentTypeAsync(string contentType);
        Task<IEnumerable<Attachment>> GetByWorkerIdAsync(int workerId);
        Task<IEnumerable<Attachment>> GetByAccidentIdAsync(long accidentId);
    }
}




