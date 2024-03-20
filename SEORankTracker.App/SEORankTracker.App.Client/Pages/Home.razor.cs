namespace SEORankTracker.App.Client.Pages;

public partial class Home
{
    [Inject]
    public required HttpClient Http { get; init; }

    [Inject]
    public required IDialogService DialogService { get; init; }

    private ProvidersResponse? _providers_response;
    private MudTable<HistoryViewModel>? _table;
    private bool _is_loading = true;

    protected override async Task OnInitializedAsync()
    {
        SendRequestResult<ProvidersResponse> results = await Http.SendRequestAsync<ProvidersResponse>(HttpMethod.Get, "providers");
        if (results.Failed is false)
        {
            _providers_response = results.Response;
        }
    }

    private async Task TestClickAsync(MouseEventArgs args)
    {
        DialogParameters<SearchDialog> parameters = new()
        {
            { x => x.SearchProvidersSelectListItems, _providers_response?.Providers.Select(x => new SelectListItem {
                Text = x,
                Value = x,
                Selected = false
            }) ?? [] }
        };

        IDialogReference dialog_reference = DialogService.Show<SearchDialog>("Submit Search Term", parameters);
        DialogResult dialog_result = await dialog_reference.Result;
        if (dialog_result.Canceled is false)
        {
            await RefreshTableAsync();
        }
    }

    public async Task RefreshTableAsync()
    {
        if (_table is not null) { await _table.ReloadServerData(); }
    }

    public async Task<TableData<HistoryViewModel>> ReloadTableAsync(TableState state)
    {
        _is_loading = true;
        try
        {
            SendRequestResult<HistoryResponse> results = await Http.SendRequestAsync<HistoryResponse>(HttpMethod.Get, $"history?PageSize={state.PageSize}&Page={state.Page}");

            return results.Failed
                ? new TableData<HistoryViewModel>() { Items = [] }
                : new TableData<HistoryViewModel>()
                    {
                        Items = results.Response.Results.Select(x => new HistoryViewModel
                        {
                            Id = x.Id,
                            Error = x.Error,
                            Occurrences = x.Occurrences,
                            RankListCsv = x.RankListCsv,
                            SearchEngine = x.SearchEngine,
                            SearchTerm = x.SearchTerm,
                            MatchTerm = x.MatchTerm,
                            Success = x.Success,
                            Url = x.Url,
                            UtcDateTime = x.UtcDateTime
                        }),
                        TotalItems = results.Response.TotalItems,
                    };
        }
        finally
        {
            _is_loading = false;
        }
    }
}
