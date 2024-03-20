namespace SEORankTracker.Logic.Providers;

[SearchProviderDescriptor("Bing")]
public sealed class BingSearchProvder : ISearchProvider
{
    public string Name { get; } = "Bing";
    public string BaseUri { get; } = "https://www.bing.com";
    public string Path { get; } = "search";
    public int Records { get; } = 10;
    public string RecordQueryTerm { get; } = "count";
    public string SearchQueryTerm { get; } = "q";
    public static string HtmlRegexFilter { get; } = @"<a class=""tilk"" href=""http(.*?)""";

    private readonly Dictionary<string, string> _headers = [];
    public IDictionary<string, string> Headers => _headers;

    public required Regex Regex { get; init; }
    public required string SearchTerm { get; init; }

    public static ISearchProvider Create(string search_term)
    {
        BingSearchProvder search_provider = new()
        {
            SearchTerm = search_term,
            Regex = new(HtmlRegexFilter, RegexOptions.Compiled, TimeSpan.FromMilliseconds(50))
        };

        search_provider._headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/122.0.0.0 Safari/537.36");
        return search_provider;
    }
}
