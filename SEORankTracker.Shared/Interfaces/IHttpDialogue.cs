namespace SEORankTracker.Shared.Interfaces;

/// <summary>
/// Represents an HTTP dialogue interface.
/// This interface is assigned to the request, and the <see cref="TResponse" /> is the defined returning response associated with the request.
/// </summary>
/// <typeparam name="TResponse">The type of response expected from the HTTP request, must implement the <see cref="IResponseModel" /> interface.</typeparam>
public interface IHttpDialogue<TResponse> : IRequest<TResponse> where TResponse : IResponseModel { }
