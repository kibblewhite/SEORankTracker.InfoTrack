namespace SEORankTracker.App.Client.Components;

public partial class SearchDialog
{
    [Inject]
    public required IServiceProvider ServiceProvider { get; init; }

    [Inject]
    public required HttpClient Http { get; init; }

    [CascadingParameter]
    public required MudDialogInstance MudDialog { get; init; }

    [Parameter]
    public required IEnumerable<SelectListItem> SearchProvidersSelectListItems { get; init; }

    protected ValidationMessageStore validation_message_store;
    protected SearchViewModel view_model;
    protected EditContext edit_context;
    protected bool is_task_running;

    public SearchDialog()
    {
        is_task_running = false;
        view_model = new();

        edit_context = new(view_model);
        edit_context.OnFieldChanged += FieldChangedEvent;
        edit_context.OnValidationRequested += ValidationRequestedEvent;

        validation_message_store = new(edit_context);
    }

    void Cancel() => MudDialog.Cancel();

    public readonly Func<SelectListItem, string> SelectListItemConverter = x => x?.Text ?? string.Empty;

    protected void FieldChangedEvent(object? sender, FieldChangedEventArgs e)
    {
        validation_message_store.Clear(e.FieldIdentifier);
        edit_context.NotifyValidationStateChanged();
    }

    protected void ValidationRequestedEvent(object? sender, ValidationRequestedEventArgs e)
    {
        SearchValidator search_validator = new();
        FluentValidation.Results.ValidationResult validation_result = search_validator.Validate(view_model);
        validation_message_store.Clear();

        foreach (FluentValidation.Results.ValidationFailure validation_failure in validation_result.Errors)
        {
            FieldIdentifier field_identifier = new(validation_failure.AttemptedValue, validation_failure.PropertyName);
            validation_message_store.Add(field_identifier, validation_failure.ErrorMessage);
        }

        edit_context.NotifyValidationStateChanged();
    }

    protected void ForceClearMessageStore() => validation_message_store.Clear();

    public async Task SubmitAsync()
    {
        is_task_running = true;

        try
        {
            SearchRequest request = new()
            {
                SearchProvider = view_model.SearchProvider,
                SearchTerm = view_model.SearchTerm
            };

            SendRequestResult<SearchResponse> results = await Http.SendRequestAsync<SearchResponse>(HttpMethod.Post, "search", request);
            if (results.Failed is false)
            {
                MudDialog.Close(DialogResult.Ok(true));
            }

            if (results.Failed is true)
            {
                // display some error to UI / toast, dialog or otherwise...
                _ = results.HttpResponseMessage.StatusCode;
            }
        }
        finally
        {
            is_task_running = false;
        }
    }
}
