namespace SEORankTracker.Logic.Handlers;

public sealed class ProvidersHandler(ILogger<ProvidersHandler> logger, SearchProviderFactory search_provider_factory) : IAsyncDialogueHandler<ProvidersRequest, ProvidersResponse>
{
    private readonly ILogger<ProvidersHandler> _logger = logger;
    private readonly SearchProviderFactory _search_provider_factory = search_provider_factory ?? throw new Exception(nameof(search_provider_factory));

    public Task<ProvidersResponse> Handle(ProvidersRequest request, CancellationToken cancellation_token)
    {
        _logger.LogTrace("ProvidersHandler called.");
        ProvidersResponse response = new()
        {
            Providers = _search_provider_factory.ProvidersNames
        };
        return Task.FromResult(response);
    }
}
