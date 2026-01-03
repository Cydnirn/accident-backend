
using AccidentBackend.Models;
using System.Threading.Tasks;

namespace AccidentBackend.Repository
{
    public interface ISafetyEquipmentRepository : IRepository<SafetyEquipment>
    {
        Task<IEnumerable<SafetyEquipment>> GetByEquipmentTypeAsync(string equipmentType);
        Task<IEnumerable<SafetyEquipment>> GetByStatusAsync(string status);
        Task<IEnumerable<SafetyEquipment>> GetBySiteIdAsync(int siteId);
        Task<SafetyEquipment?> GetByTagNumberAsync(string tagNumber);
    }
}


