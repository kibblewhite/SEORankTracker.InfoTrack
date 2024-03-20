namespace SEORankTracker.Shared.Interfaces;

public interface IAsyncRequestHandler<TRequest> : IRequestHandler<TRequest>
    where TRequest : IHttpRequest
{ }
