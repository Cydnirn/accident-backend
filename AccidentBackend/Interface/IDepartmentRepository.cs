using AccidentBackend.Models;
using System.Threading.Tasks;
namespace AccidentBackend.Interface{
    public interface IDepartmentRepository : IRepository<Department>
    {
        Task<IEnumerable<Department>> GetDepartmentsWithWorkersAsync();
        Task<Department?> GetDepartmentWithWorkersAsync(int id);
    }
}




