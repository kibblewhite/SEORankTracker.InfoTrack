namespace SEORankTracker.Shared.Validators;

public sealed class SearchValidator : AbstractValidator<SearchRequest>
{
    public SearchValidator()
    {
        RuleFor(x => x.SearchProvider).NotEmpty();
        RuleFor(x => x.SearchTerm).NotEmpty();
    }
}
