namespace SEORankTracker.Frontend.Validators;

public class SearchValidator : AbstractValidator<SearchViewModel>
{
    public SearchValidator()
    {
        RuleFor(x => x.SearchProvider).NotEmpty();
        RuleFor(x => x.SearchTerm).NotEmpty();
    }
}
