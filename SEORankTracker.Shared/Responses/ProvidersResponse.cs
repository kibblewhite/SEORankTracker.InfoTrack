namespace SEORankTracker.Shared.Responses;

public sealed class ProvidersResponse : IResponseModel
{
    public required List<string> Providers { get; init; }
}
