using System.Collections.Generic;
using System.Threading.Tasks;
using AccidentBackend.Models;

namespace AccidentBackend.Repository
{
    public interface ISiteRepository : IRepository<Site>
    {
        Task<Site?> GetBySiteCodeAsync(string siteCode);
        Task<IEnumerable<Site>> GetBySiteTypeAsync(string siteType);
        Task<Site?> GetSiteWithWorkersAsync(int id);
        Task<Site?> GetSiteWithAccidentsAsync(int id);
        Task<Site?> GetSiteWithDetailsAsync(int id);
    }
}

