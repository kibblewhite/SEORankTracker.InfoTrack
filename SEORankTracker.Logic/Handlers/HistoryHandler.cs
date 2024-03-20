namespace SEORankTracker.Logic.Handlers;

public sealed class HistoryHandler(ApplicationDatabaseContext application_database_context) : IAsyncDialogueHandler<HistoryRequest, HistoryResponse>
{
    private readonly ApplicationDatabaseContext _application_database_context = application_database_context;

    public async Task<HistoryResponse> Handle(HistoryRequest request, CancellationToken cancellation_token)
    {
        // Calculate the skip count based on page number and page size
        int skip_count = request.Page * request.PageSize;

        // Query the database to retrieve the paginated entries and total count
        IQueryable<SearchEntry> query = _application_database_context.SearchEntries.AsQueryable();

        // Retrieve the total count of entries that match the lookup criteria
        int total_items = await query.CountAsync(cancellation_token);

        // Apply pagination to the query
        List<SearchEntry> entries = await query
            .OrderByDescending(entry => entry.UtcDateTime)
            .Skip(skip_count)
            .Take(request.PageSize)
            .ToListAsync(cancellation_token);

        HistoryResponse response = new()
        {
            Results = entries.Select(x => new HistoryResponseModel
            {
                Id = x.Id,
                Error = x.Error,
                Occurrences = x.Occurrences,
                RankListCsv = x.RankListCsv,
                SearchEngine = x.SearchEngine,
                SearchTerm = x.SearchTerm,
                MatchTerm = x.MatchTerm,
                Success = x.Success,
                Url = x.Url,
                UtcDateTime = x.UtcDateTime
            }).ToList(),
            TotalItems = total_items
        };

        return response;
    }
}
