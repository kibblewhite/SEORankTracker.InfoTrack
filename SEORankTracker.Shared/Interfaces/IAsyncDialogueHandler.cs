namespace SEORankTracker.Shared.Interfaces;

public interface IAsyncDialogueHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TResponse : IResponseModel
    where TRequest : IHttpDialogue<TResponse>
{ }
