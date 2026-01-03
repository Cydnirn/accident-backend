using AccidentBackend.Models;
using System.Threading.Tasks;
namespace AccidentBackend.Repository{
    public interface IDepartmentRepository : IRepository<Department>
    {
        Task<IEnumerable<Department>> GetDepartmentsWithWorkersAsync();
        Task<Department?> GetDepartmentWithWorkersAsync(int id);
    }
}




