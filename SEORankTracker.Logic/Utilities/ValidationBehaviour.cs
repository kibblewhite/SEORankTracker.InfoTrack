namespace SEORankTracker.Logic.Utilities;

public class ValidationPreProcessor<TRequest>(IValidator<TRequest> validator) : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly IValidator<TRequest> _validator = validator;

    /// <remarks>The ValidateAndThrowAsync will be caught/handled by the ValidationMappingMiddleware</remarks>
    /// <param name="request"></param>
    /// <param name="cancellation_token"></param>
    /// <returns></returns>
    public async Task Process(TRequest request, CancellationToken cancellation_token)
        => await _validator.ValidateAndThrowAsync(request, cancellation_token);
}
