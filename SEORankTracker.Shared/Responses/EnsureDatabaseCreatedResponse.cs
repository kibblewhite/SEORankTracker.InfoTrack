namespace SEORankTracker.Shared.Responses;

public sealed class EnsureDatabaseCreatedResponse : IResponseModel
{
    public required bool Created { get; init; }
}
