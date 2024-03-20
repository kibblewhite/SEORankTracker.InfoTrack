namespace SEORankTracker.Logic.Handlers;

public sealed class SearchHandler(IConfiguration configuration, SearchProviderFactory search_provider_factory, ApplicationDatabaseContext application_database_context) : IAsyncDialogueHandler<SearchRequest, SearchResponse>
{
    private readonly IConfiguration _configuration = configuration;
    private readonly SearchProviderFactory _search_provider_factory = search_provider_factory;
    private readonly ApplicationDatabaseContext _application_database_context = application_database_context;

    public async Task<SearchResponse> Handle(SearchRequest request, CancellationToken cancellation_token)
    {
        string match_term = _configuration.GetValue<string>("MatchTerm") ?? "www.infotrack.co.uk";

        ISearchProvider provider = _search_provider_factory.InstantiateFromName(request.SearchProvider, request.SearchTerm);
        SearchProviderResult search_provider_result = await provider.SearchProviderProcessAsync(match_term, cancellation_token);

        SearchEntry search_entry = search_provider_result.Adapt<SearchEntry>();

        await _application_database_context.AddAsync(search_entry, cancellation_token);
        await _application_database_context.SaveChangesAsync(cancellation_token);

        return await Task.FromResult(new SearchResponse { });
    }
}
