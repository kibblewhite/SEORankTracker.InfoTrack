namespace SEORankTracker.Shared.Requests;

public sealed class HistoryRequest : IHttpDialogue<HistoryResponse>
{
    public required int Page { get; init; }
    public required int PageSize { get; init; }
}
