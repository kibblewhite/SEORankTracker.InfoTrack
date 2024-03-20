//namespace SEORankTracker.Logic.Providers;

//[SearchProviderDescriptor("Ecosia")]
//public sealed class EcosiaSearchProvder : ISearchProvider
//{
//    public string Name { get; } = "Ecosia";
//    public string BaseUri { get; } = "https://www.ecosia.org";
//    public string Path { get; } = "search";
//    public int Records { get; } = 10;
//    public string RecordQueryTerm { get; } = "count";
//    public string SearchQueryTerm { get; } = "q";
//    public static string HtmlRegexFilter { get; } = @"<a data-test-id=""result__info"" tabindex=""-1"" href=""http(.*?)""";

//    private readonly Dictionary<string, string> _headers = [];
//    public IDictionary<string, string> Headers => _headers;

//    public required Regex Regex { get; init; }
//    public required string SearchTerm { get; init; }

//    public static ISearchProvider Create(string search_term) => new EcosiaSearchProvder
//    {
//        SearchTerm = search_term,
//        Regex = new(HtmlRegexFilter, RegexOptions.Compiled, TimeSpan.FromMilliseconds(50))
//    };
//}
