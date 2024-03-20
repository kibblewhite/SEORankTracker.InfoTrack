namespace SEORankTracker.Logic.Extensions;

public static class SearchProviderExtensions
{
    public static async Task<SearchProviderResult> SearchProviderProcessAsync(this ISearchProvider provider, string match_term, CancellationToken cancellation_token)
    {
        HttpResponseMessageResult results = await provider.GetHttpResponseMessageAsync(cancellation_token);
        if (results.Failed)
        {
            SearchProviderResult failed_result = new()
            {
                Url = provider.BuildUrl(),
                SearchEngine = provider.Name,
                SearchTerm = provider.SearchTerm,
                MatchTerm = match_term,
                RankListCsv = string.Empty,
                Occurrences = 0,
                Success = false,
                Error = results.Error,
                UtcDateTime = DateTimeOffset.UtcNow
            };
            return failed_result;
        }

        string html = await results.Value.Content.ReadAsStringAsync(cancellation_token);
        MatchCollection matches = provider.Regex.Matches(html);

        HashSet<int> indices = [];
        for (int idx = 0; idx < matches.Count; idx++)
        {
            if (matches[idx].Value.Contains(match_term, StringComparison.OrdinalIgnoreCase))
            {
                indices.Add(idx + 1);
            }
        }

        List<int> rank_list = [.. indices];

        SearchProviderResult result = new()
        {
            Url = provider.BuildUrl(),
            SearchEngine = provider.Name,
            SearchTerm = provider.SearchTerm,
            MatchTerm = match_term,
            RankListCsv = string.Join(", ", rank_list),
            Occurrences = rank_list.Count,
            Success = true,
            Error = results.Error,
            UtcDateTime = DateTimeOffset.UtcNow
        };

        return result;
    }

    public static async Task<HttpResponseMessageResult> GetHttpResponseMessageAsync(this ISearchProvider provider, CancellationToken cancellation_token)
    {
        using HttpClient client = new();
        foreach (KeyValuePair<string, string> header in provider.Headers)
        {
            client.DefaultRequestHeaders.Add(header.Key, header.Value);
        }

        string url = provider.BuildUrl();
        HttpRequestMessage request = new(HttpMethod.Get, url);
        HttpResponseMessage response = await client.SendAsync(request, cancellation_token);

        return response.IsSuccessStatusCode is true
            ? HttpResponseMessageResult.Success(response)
            : HttpResponseMessageResult.Failure($"Request to {url} failed with status {response.StatusCode}");
    }
}
