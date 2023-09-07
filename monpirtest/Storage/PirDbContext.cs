using Microsoft.EntityFrameworkCore;
using monpirtest.Model;

namespace monpirtest.Storage;

public class PirDbContext : DbContext
{
    public PirDbContext(DbContextOptions<PirDbContext> options) : base(options)
    {
    }

    public DbSet<Project> Projects { get; set; }
    public DbSet<ObjectPir> ObjectPir { get; set; }
    public DbSet<DocumentationRd> DocumentationRd { get; set; }
    public DbSet<DocumentationPd> DocumentationPd { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("PirDatabase");

            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}