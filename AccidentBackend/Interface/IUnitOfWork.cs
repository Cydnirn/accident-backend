using System;
using System.Threading.Tasks;

namespace AccidentBackend.Repository;

/// <summary>
/// Unit of Work pattern to coordinate repository operations and transactions
/// </summary>
public interface IUnitOfWork : IDisposable
{
    // Main entity repositories
    IAccidentRepository Accidents { get; }
    IWorkerRepository Workers { get; }
    ISiteRepository Sites { get; }
    IDepartmentRepository Departments { get; }

    // Lookup repositories
    IHazardTypeRepository HazardTypes { get; }
    IAccidentCauseRepository AccidentCauses { get; }
    ISafetyEquipmentRepository SafetyEquipments { get; }
    IShiftRepository Shifts { get; }

    // Related entity repositories
    IAccidentParticipantRepository AccidentParticipants { get; }
    IAccidentEquipmentRepository AccidentEquipments { get; }
    IWitnessRepository Witnesses { get; }
    IActionTakenRepository ActionsTaken { get; }
    IAttachmentRepository Attachments { get; }

    /// <summary>
    /// Save all changes made in this unit of work
    /// </summary>
    Task<int> SaveChangesAsync();

    /// <summary>
    /// Begin a database transaction
    /// </summary>
    Task BeginTransactionAsync();

    /// <summary>
    /// Commit the current transaction
    /// </summary>
    Task CommitTransactionAsync();

    /// <summary>
    /// Rollback the current transaction
    /// </summary>
    Task RollbackTransactionAsync();
}

