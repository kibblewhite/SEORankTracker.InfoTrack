namespace SEORankTracker.Shared.Responses;

public sealed class HistoryResponse : IResponseModel
{
    public required List<HistoryResponseModel> Results { get; init; }
    public required int TotalItems { get; init; }
}
