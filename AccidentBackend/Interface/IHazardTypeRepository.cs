
using AccidentBackend.Models;
namespace AccidentBackend.Interface
{
    public interface IHazardTypeRepository : IRepository<HazardType>
    {
        Task<HazardType?> GetByCodeAsync(string code);
    }
}



