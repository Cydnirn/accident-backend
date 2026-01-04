using AccidentBackend.Models;
using System.Threading.Tasks;

namespace AccidentBackend.Interface
{
    public interface IAccidentEquipmentRepository : IRepository<AccidentEquipment>
    {
        Task<IEnumerable<AccidentEquipment>> GetByAccidentIdAsync(long accidentId);
        Task<IEnumerable<AccidentEquipment>> GetByEquipmentIdAsync(int equipmentId);
    }
}