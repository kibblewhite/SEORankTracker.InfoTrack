namespace SEORankTracker.Shared.Utilities;

public class HttpResponseMessageResult
{
    public required HttpResponseMessage Value { get; init; }

    public required bool Failed { get; init; }

    public required string Error { get; init; }

    public static HttpResponseMessageResult Failure(string error) => new()
    {
        Failed = true,
        Value = default!,
        Error = error
    };

    public static HttpResponseMessageResult Success(HttpResponseMessage value) => new()
    {
        Failed = false,
        Value = value,
        Error = string.Empty
    };

}
