namespace SEORankTracker.Logic.Providers;

public interface ISearchProvider
{
    string Name { get; }
    string BaseUri { get; }
    string Path { get; }
    int Records { get; }
    string RecordQueryTerm { get; }
    string SearchQueryTerm { get; }
    string SearchTerm { get; init; }
    IDictionary<string, string> Headers { get; }

    abstract static string HtmlRegexFilter { get; }
    Regex Regex { get; init; }

    static abstract ISearchProvider Create(string search_term);

    string BuildUrl()
    {
        Uri base_uri = new(BaseUri);
        UriBuilder uri_builder = new(base_uri)
        {
            Path = Path
        };

        System.Collections.Specialized.NameValueCollection query = HttpUtility.ParseQueryString(string.Empty);
        query[RecordQueryTerm] = Convert.ToString(Records);
        query[SearchQueryTerm] = SearchTerm;

        uri_builder.Query = query.ToString();
        string url = uri_builder.ToString();
        return url;
    }
}
