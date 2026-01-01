# AccidentBackend - SQLite ORM Implementation
This class library implements a comprehensive SQLite database using Entity Framework Core ORM for accident management and tracking in industrial settings.
## Database Schema
The database models the following entities based on `database.md`:
### Core Entities
- **Sites**: Physical locations (oil rigs, factories, etc.)
- **Departments**: Organizational departments with optional manager references
- **Shifts**: Work shifts with time ranges
- **Workers**: Employees and contractors with department and site assignments
### Lookup Tables
- **HazardTypes**: Hazard classifications (with unique codes)
- **AccidentCauses**: Root cause classifications (with unique codes)
### Equipment
- **SafetyEquipment**: Equipment inventory with inspection tracking
### Accident Transaction System
- **Accidents**: Main transaction table for accident records with comprehensive tracking
- **AccidentParticipants**: Workers involved in accidents (injured, operators, supervisors)
- **AccidentEquipment**: Equipment involved in accidents (many-to-many relationship)
- **Witnesses**: Witness statements and contact information
- **ActionsTaken**: Actions performed in response to accidents
- **Attachments**: Files and documents related to accidents
## Installation
The project requires:
- **.NET 9.0**
- **Microsoft.EntityFrameworkCore.Sqlite** (v9.0.0)
- **Microsoft.EntityFrameworkCore.Design** (v9.0.0)
- **Microsoft.Extensions.Logging** (v10.0.1)
## Usage
### 1. Basic Setup (Console Application)
```csharp
using AccidentBackend.Data;
// Create the database context
var connectionString = "Data Source=accidents.db";
var dbContext = DbContextConfiguration.CreateDbContext(connectionString);
// Initialize the database
await dbContext.Database.EnsureCreatedAsync();
// Create the backend service
var backend = new AccidentBackend.AccidentBackend(dbContext);
await backend.InitializeDatabaseAsync();
```
### 2. Dependency Injection Setup (ASP.NET Core, Worker Service)
```csharp
using AccidentBackend.Data;
using Microsoft.Extensions.DependencyInjection;
var services = new ServiceCollection();
// Add the database context
services.AddAccidentDatabase("Data Source=accidents.db");
// Add logging
services.AddLogging();
// Add the backend service
services.AddScoped<AccidentBackend.AccidentBackend>();
var serviceProvider = services.BuildServiceProvider();
```
### 3. Creating Records
```csharp
using AccidentBackend.Models;
// Create a site
var site = new Site
{
    Name = "Oil Rig Alpha",
    SiteCode = "ORA-001",
    Location = "North Sea",
    SiteType = "Oil Rig",
    ContactNumber = "+44-123-456-7890"
};
await dbContext.Sites.AddAsync(site);
await dbContext.SaveChangesAsync();
// Create a worker
var worker = new Worker
{
    EmployeeNumber = "EMP-12345",
    FirstName = "John",
    LastName = "Doe",
    Email = "john.doe@example.com",
    CurrentSiteId = site.Id,
    HireDate = DateTime.Now.AddYears(-5),
    IsContractor = false
};
await dbContext.Workers.AddAsync(worker);
await dbContext.SaveChangesAsync();
// Create an accident
var accident = new Accident
{
    AccidentNumber = "AR-2026-0001",
    SiteId = site.Id,
    OccurredAt = DateTime.Now.AddHours(-2),
    ReportedByWorkerId = worker.Id,
    SeverityLevel = 3,
    IsFatal = false,
    Description = "Worker slipped on wet surface near drilling platform",
    Status = "Under Investigation"
};
await dbContext.Accidents.AddAsync(accident);
await dbContext.SaveChangesAsync();
// Add accident participant
var participant = new AccidentParticipant
{
    AccidentId = accident.Id,
    WorkerId = worker.Id,
    Role = "Injured Party",
    Injured = true,
    Notes = "Minor injuries to left ankle"
};
await dbContext.AccidentParticipants.AddAsync(participant);
await dbContext.SaveChangesAsync();
```
### 4. Querying Data
```csharp
using Microsoft.EntityFrameworkCore;
// Get all accidents for a specific site
var siteAccidents = await dbContext.Accidents
    .Where(a => a.SiteId == siteId)
    .Include(a => a.Site)
    .Include(a => a.ReportedByWorker)
    .Include(a => a.Participants)
        .ThenInclude(p => p.Worker)
    .ToListAsync();
// Complex query with filtering
var severeAccidents = await dbContext.Accidents
    .Where(a => a.SeverityLevel >= 4)
    .Include(a => a.Site)
    .Include(a => a.Participants)
        .ThenInclude(p => p.Worker)
    .Include(a => a.Witnesses)
    .Include(a => a.ActionsTaken)
    .OrderByDescending(a => a.OccurredAt)
    .ToListAsync();
// Get accident statistics
var accidentCount = await dbContext.Accidents
    .Where(a => a.OccurredAt >= DateTime.Now.AddMonths(-1))
    .CountAsync();
var fatalAccidents = await dbContext.Accidents
    .Where(a => a.IsFatal)
    .CountAsync();
```
### 5. Using the Backend Service
```csharp
// Create accident using the backend service
var accident = new Accident
{
    AccidentNumber = "AR-2026-0002",
    SiteId = siteId,
    OccurredAt = DateTime.Now,
    SeverityLevel = 2,
    Description = "Equipment malfunction"
};
var createdAccident = await backend.CreateAccidentAsync(accident);
// Get accidents by site
var accidents = await backend.GetAccidentsBySiteAsync(siteId);
```
## Connection String Examples
### SQLite File Database
```
Data Source=accidents.db
```
### SQLite with Full Path
```
Data Source=/var/data/accidents.db
```
### SQLite In-Memory Database (for testing)
```
Data Source=:memory:
```
## Database Migrations (Optional)
To use EF Core migrations for schema updates:
```bash
# Install EF Core tools globally (if not already installed)
dotnet tool install --global dotnet-ef
# Add initial migration
dotnet ef migrations add InitialCreate --project AccidentBackend
# Apply migrations to database
dotnet ef database update --project AccidentBackend
# Add a new migration after model changes
dotnet ef migrations add AddNewFeature --project AccidentBackend
dotnet ef database update --project AccidentBackend
```
## Key Features
### Entity Framework Core ORM
- Full LINQ support for complex queries
- Automatic SQL generation
- Change tracking and lazy loading support
### Relationship Management
- Automatic navigation property handling
- Eager loading with `.Include()`
- Lazy loading support (if configured)
### Data Integrity
- **Cascade Deletes**: Configured for child records (participants, witnesses, actions, attachments)
- **Set Null**: Used for optional foreign keys to prevent orphaned records
- **Restrict**: Used for critical relationships (Accident → Site)
### Performance Optimizations
- **Indexes**: Created on frequently queried fields:
  - Accident: SiteId, OccurredAt, SeverityLevel, Status
  - Worker: EmployeeNumber
  - Site: SiteCode
  - Equipment: TagNumber
  - HazardType, AccidentCause: Code
### Unique Constraints
Prevents duplicate records:
- Site codes
- Employee numbers
- Equipment tag numbers
- Accident numbers
- Hazard/cause codes
### Default Values
Automatic timestamps using SQLite functions:
- CreatedAt, ReportedAt, UploadedAt, RecordedAt
## Model Relationships
### One-to-Many
- `Site` → `Accidents`
- `Site` → `Workers`
- `Site` → `SafetyEquipment`
- `Worker` → `ReportedAccidents`
- `Accident` → `Participants`
- `Accident` → `Witnesses`
- `Accident` → `ActionsTaken`
- `Accident` → `Attachments`
### Many-to-Many (through junction tables)
- `Accident` ↔ `SafetyEquipment` (via AccidentEquipment)
### Self-Referential
- `Department.ManagerWorkerId` → `Worker`
## Notes
- All timestamps use `DateTime` type (stored as TEXT in SQLite ISO 8601 format)
- The `Department.ManagerWorkerId` is nullable to avoid circular FK constraints
- Delete behaviors are carefully configured to maintain referential integrity
- Foreign key relationships use nullable integers where appropriate
- The Accident entity uses `long` for Id to support large numbers of records
## Example Project Structure
```
AccidentBackend/
├── Models/
│   ├── Accident.cs
│   ├── AccidentCause.cs
│   ├── AccidentEquipment.cs
│   ├── AccidentParticipant.cs
│   ├── ActionTaken.cs
│   ├── Attachment.cs
│   ├── Department.cs
│   ├── HazardType.cs
│   ├── SafetyEquipment.cs
│   ├── Shift.cs
│   ├── Site.cs
│   ├── Witness.cs
│   └── Worker.cs
├── Data/
│   ├── AccidentDbContext.cs
│   └── DbContextConfiguration.cs
├── AccidentBackend.cs
└── README.md
```
## Support
For issues or questions, refer to:
- Entity Framework Core documentation: https://docs.microsoft.com/ef/core/
- SQLite documentation: https://www.sqlite.org/docs.html
