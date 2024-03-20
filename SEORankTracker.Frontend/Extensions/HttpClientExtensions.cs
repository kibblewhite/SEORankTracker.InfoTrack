namespace SEORankTracker.Frontend.Extensions;

public static class HttpClientExtensions
{
    public static async Task<SendRequestResult<T>> SendRequestAsync<T>(this HttpClient client, HttpMethod method, string request_uri, object? post_payload = null) where T : IResponseModel
    {
        HttpRequestMessage request = new(method, request_uri);
        if (method.Equals(HttpMethod.Post) && post_payload is not null)
        {
            request.Content = JsonContent.Create(post_payload);
        }

        using HttpResponseMessage http_response_message = await client.SendAsync(request);

        // "application/problem+json"
        if (http_response_message.IsSuccessStatusCode is true)
        {
            T? response_content = await http_response_message.Content.ReadFromJsonAsync<T>();
            return response_content != null
                ? SendRequestResult<T>.Success(response_content, http_response_message)
                : SendRequestResult<T>.Failure(http_response_message);
        }

        return SendRequestResult<T>.Failure(http_response_message);
    }
}
