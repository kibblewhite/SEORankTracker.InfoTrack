namespace SEORankTracker.Shared.Validators;

public sealed class HistoryValidator : AbstractValidator<HistoryRequest>
{
    public HistoryValidator()
    {
        RuleFor(x => x.PageSize).GreaterThan(0);
        RuleFor(x => x.Page).GreaterThan(-1);
    }
}
