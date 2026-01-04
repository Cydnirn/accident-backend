using AccidentBackend.Data;
using AccidentBackend.Interface;
using AccidentBackend.Models;

namespace AccidentBackend.Repository
{
    public class ShiftRepository : Repository<Shift>, IShiftRepository
    {
        public ShiftRepository(AccidentDbContext context) : base(context)
        {
        }
    }
}

