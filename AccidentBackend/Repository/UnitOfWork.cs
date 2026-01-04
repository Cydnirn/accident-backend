using System;
using System.Threading.Tasks;
using AccidentBackend.Interface;
using AccidentBackend.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace AccidentBackend.Repository
{
    /// <summary>
    /// Unit of Work implementation to coordinate repository operations and transactions
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AccidentDbContext _context;
        private IDbContextTransaction? _transaction;

        // Lazy initialization for repositories
        private IAccidentRepository? _accidents;
        private IWorkerRepository? _workers;
        private ISiteRepository? _sites;
        private IDepartmentRepository? _departments;
        private IHazardTypeRepository? _hazardTypes;
        private IAccidentCauseRepository? _accidentCauses;
        private ISafetyEquipmentRepository? _safetyEquipments;
        private IShiftRepository? _shifts;
        private IAccidentParticipantRepository? _accidentParticipants;
        private IAccidentEquipmentRepository? _accidentEquipments;
        private IWitnessRepository? _witnesses;
        private IActionTakenRepository? _actionsTaken;
        private IAttachmentRepository? _attachments;

        public UnitOfWork(AccidentDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IAccidentRepository Accidents
        {
            get { return _accidents ??= new AccidentRepository(_context); }
        }

        public IWorkerRepository Workers
        {
            get { return _workers ??= new WorkerRepository(_context); }
        }

        public ISiteRepository Sites
        {
            get { return _sites ??= new SiteRepository(_context); }
        }

        public IDepartmentRepository Departments
        {
            get { return _departments ??= new DepartmentRepository(_context); }
        }

        public IHazardTypeRepository HazardTypes
        {
            get { return _hazardTypes ??= new HazardTypeRepository(_context); }
        }

        public IAccidentCauseRepository AccidentCauses
        {
            get { return _accidentCauses ??= new AccidentCauseRepository(_context); }
        }

        public ISafetyEquipmentRepository SafetyEquipments
        {
            get { return _safetyEquipments ??= new SafetyEquipmentRepository(_context); }
        }

        public IShiftRepository Shifts
        {
            get { return _shifts ??= new ShiftRepository(_context); }
        }

        public IAccidentParticipantRepository AccidentParticipants
        {
            get { return _accidentParticipants ??= new AccidentParticipantRepository(_context); }
        }

        public IAccidentEquipmentRepository AccidentEquipments
        {
            get { return _accidentEquipments ??= new AccidentEquipmentRepository(_context); }
        }

        public IWitnessRepository Witnesses
        {
            get { return _witnesses ??= new WitnessRepository(_context); }
        }

        public IActionTakenRepository ActionsTaken
        {
            get { return _actionsTaken ??= new ActionTakenRepository(_context); }
        }

        public IAttachmentRepository Attachments
        {
            get { return _attachments ??= new AttachmentRepository(_context); }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }
}

