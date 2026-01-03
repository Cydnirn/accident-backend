namespace AccidentBackend;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Models;
using Repository;
using Microsoft.Extensions.Logging;

public class AccidentBackend
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly AccidentDbContext _dbContext;
    private readonly ILogger<AccidentBackend>? _logger;
    private static AccidentBackend? _instance = null;
    public IUnitOfWork Repository => _unitOfWork;

    private AccidentBackend(AccidentDbContext dbContext, ILogger<AccidentBackend>? logger = null)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _unitOfWork = new UnitOfWork(dbContext);
        _logger = logger;
    }

    public static AccidentBackend CreateInstance(AccidentDbContext dbContext, ILogger<AccidentBackend>? logger = null)
    {
        return _instance != null ? _instance : new AccidentBackend(dbContext, logger);
    }

    /// <summary>
    /// Initialize the database and ensure schema is created
    /// </summary>
    public async Task InitializeDatabaseAsync()
    {
        await _dbContext.Database.EnsureCreatedAsync();
        _logger?.LogInformation("Database initialized successfully");
    }
}