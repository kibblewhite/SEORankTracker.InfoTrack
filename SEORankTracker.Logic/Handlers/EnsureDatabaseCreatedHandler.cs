namespace SEORankTracker.Logic.Handlers;

public sealed class EnsureDatabaseCreatedHandler(ILogger<EnsureDatabaseCreatedHandler> logger, ApplicationDatabaseContext application_database_context) : IAsyncDialogueHandler<EnsureDatabaseCreatedRequest, EnsureDatabaseCreatedResponse>
{
    private readonly ILogger<EnsureDatabaseCreatedHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly ApplicationDatabaseContext _application_database_context = application_database_context ?? throw new Exception(nameof(application_database_context));

    public async Task<EnsureDatabaseCreatedResponse> Handle(EnsureDatabaseCreatedRequest request, CancellationToken cancellation_token)
    {
        _logger.LogInformation("Creating database...");
        bool created = await _application_database_context.Database.EnsureCreatedAsync(cancellation_token);
        _logger.LogInformation("Result: {created}", created);
        return await Task.FromResult(new EnsureDatabaseCreatedResponse { Created = created });
    }
}
