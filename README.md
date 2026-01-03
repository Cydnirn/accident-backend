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
using AccidentBackend;
// Create the database context
var connectionString = "Data Source=accidents.db";
var dbContext = DbContextConfiguration.CreateDbContext(connectionString);
// Initialize the database
await dbContext.Database.EnsureCreatedAsync();
// Create the backend service
var backend = new AccidentBackend.CreateInstance(dbContext);
await backend.InitializeDatabaseAsync();
```
### 2. Creating Records
```csharp
var backend = new AccidentBackend.CreateInstance(dbContext);
// Create a new site
var site = new Site
{
    SiteCode = "OR-001",
    Name = "Offshore Rig 1",
    Location = "Gulf of Mexico",
    Description = "Primary offshore drilling rig"
};
var createdSite = await backend.Repository.Sites.AddAsync(site);

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

