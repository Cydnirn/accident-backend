using AccidentBackend.Data;
using AccidentBackend.Models;

namespace AccidentBackend.Example;

public class Example
{
    private readonly AccidentDbContext _context;
    private AccidentBackend backend;

    public Example(AccidentDbContext context)
    {
        _context = context;
    }

    public async void Run()
    {
        try
        {
            backend = AccidentBackend.CreateInstance(_context);
            await backend.InitializeDatabaseAsync();
            var site = new Site{
                Name =  "Site",
                Latitude = 251.21,
                Longitude = 123.45,
                SiteCode = "SIT001",
                Type = "Construction",
                ContactNumber = "123-456-7890",
            };
            await backend.Repository.Sites.AddAsync(site);
            await backend.Repository.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }
}