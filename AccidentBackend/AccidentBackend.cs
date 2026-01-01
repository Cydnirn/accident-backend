namespace AccidentBackend;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class AccidentBackend
{
    private readonly AccidentDbContext _dbContext;
    private readonly ILogger<AccidentBackend>? _logger;

    public AccidentBackend(AccidentDbContext dbContext, ILogger<AccidentBackend>? logger = null)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _logger = logger;
    }

    public void ProcessAccident()
    {
        _logger?.LogInformation("Processing accident data...");
        Console.WriteLine("Processing accident data...");
    }

    /// <summary>
    /// Example method to create a new accident record
    /// </summary>
    public async Task<Accident> CreateAccidentAsync(Accident accident)
    {
        _dbContext.Accidents.Add(accident);
        await _dbContext.SaveChangesAsync();
        _logger?.LogInformation("Created accident with ID: {AccidentId}", accident.Id);
        return accident;
    }

    /// <summary>
    /// Example method to get all accidents for a site
    /// </summary>
    public async Task<List<Accident>> GetAccidentsBySiteAsync(int siteId)
    {
        return await _dbContext.Accidents
            .Where(a => a.SiteId == siteId)
            .Include(a => a.Site)
            .Include(a => a.ReportedByWorker)
            .Include(a => a.Participants)
            .ToListAsync();
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