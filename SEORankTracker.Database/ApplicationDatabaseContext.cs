namespace SEORankTracker.Database;

public class ApplicationDatabaseContext : DbContext
{
    public DbSet<SearchEntry> SearchEntries { get; set; }

    public ApplicationDatabaseContext(DbContextOptions<ApplicationDatabaseContext> options) : base(options)
        => SearchEntries = Set<SearchEntry>();

    protected override void OnModelCreating(ModelBuilder model_builder)
    {
        model_builder.ApplyConfiguration(new SearchEntryConfiguration());
        base.OnModelCreating(model_builder);
    }
}