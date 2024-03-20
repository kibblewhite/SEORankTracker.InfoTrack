namespace SEORankTracker.Logic.Providers;

public sealed class SearchProviderDescriptor
{
    public required string Name { get; init; }
    public required Type ProviderType { get; init; }
}
