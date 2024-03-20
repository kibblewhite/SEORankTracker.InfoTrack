namespace SEORankTracker.Logic.Models;

public sealed class SearchProviderResult
{
    public required string Url { get; init; }
    public required string SearchEngine { get; init; }
    public required string SearchTerm { get; init; }
    public required string MatchTerm { get; init; }
    public required string RankListCsv { get; init; }
    public required int Occurrences { get; init; }
    public required bool Success { get; init; }
    public required string Error { get; init; }
    public required DateTimeOffset UtcDateTime { get; init; }
}
