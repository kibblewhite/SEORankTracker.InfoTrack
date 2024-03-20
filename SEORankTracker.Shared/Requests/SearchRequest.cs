namespace SEORankTracker.Shared.Requests;

public sealed class SearchRequest : IHttpDialogue<SearchResponse>
{
    public required string SearchProvider { get; init; }

    public required string SearchTerm { get; init; }
}
