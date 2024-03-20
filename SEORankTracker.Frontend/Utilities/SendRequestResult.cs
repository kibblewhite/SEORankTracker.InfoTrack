namespace SEORankTracker.Frontend.Utilities;

public class SendRequestResult<T> where T : IResponseModel
{
    public required T Response { get; init; }
    public required HttpResponseMessage HttpResponseMessage { get; init; }
    public required bool Failed { get; init; }

    public static SendRequestResult<T> Success(T response, HttpResponseMessage http_response_message)
        => new()
        {
            Failed = false,
            HttpResponseMessage = http_response_message,
            Response = response
        };

    public static SendRequestResult<T> Failure(HttpResponseMessage http_response_message)
        => new()
        {
            Failed = true,
            HttpResponseMessage = http_response_message,
            Response = default!
        };
}
