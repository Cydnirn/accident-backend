
using AccidentBackend.Models;
namespace AccidentBackend.Repository{
    public interface IHazardTypeRepository : IRepository<HazardType>
    {
        Task<HazardType?> GetByCodeAsync(string code);
    }
}



