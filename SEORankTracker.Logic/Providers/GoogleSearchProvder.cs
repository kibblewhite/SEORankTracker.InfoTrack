namespace SEORankTracker.Logic.Providers;

[SearchProviderDescriptor("Google")]
public sealed class GoogleSearchProvder : ISearchProvider
{
    public string Name { get; } = "Google";
    public string BaseUri { get; } = "https://www.google.co.uk";
    public string Path { get; } = "search";
    public int Records { get; } = 99;
    public string RecordQueryTerm { get; } = "num";
    public string SearchQueryTerm { get; } = "q";
    public static string HtmlRegexFilter => @"(?<=<div class=""egMi0 kCrYT""><a href=""/url\?q=)[^""]*";

    private readonly Dictionary<string, string> _headers = [];
    public IDictionary<string, string> Headers => _headers;

    public required Regex Regex { get; init; }
    public required string SearchTerm { get; init; }

    public static ISearchProvider Create(string search_term) => new GoogleSearchProvder
    {
        SearchTerm = search_term,
        Regex = new(HtmlRegexFilter, RegexOptions.Compiled, TimeSpan.FromMilliseconds(50))
    };
}
