namespace SEORankTracker.Frontend.ViewModels;

public sealed class SearchViewModel
{
    private SelectListItem? _search_provider_select_list_item;
    public string SearchProvider => _search_provider_select_list_item?.Value ?? string.Empty;

    private string? _search_term;
    public string SearchTerm => _search_term ?? string.Empty;

    public void OnSearchProviderChangedEvent(SelectListItem value) => _search_provider_select_list_item = value;
    public void OnSearchTermChangedEvent(string value) => _search_term = value;
}
