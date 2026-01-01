using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace AccidentBackend.Data
{
    public static class DbContextConfiguration
    {
        public static IServiceCollection AddAccidentDatabase(
            this IServiceCollection services, 
            string connectionString)
        {
            services.AddDbContext<AccidentDbContext>(options =>
                options.UseSqlite(connectionString));
            return services;
        }
        public static AccidentDbContext CreateDbContext(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AccidentDbContext>();
            optionsBuilder.UseSqlite(connectionString);
            return new AccidentDbContext(optionsBuilder.Options);
        }
    }
}
