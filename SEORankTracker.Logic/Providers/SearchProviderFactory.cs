namespace SEORankTracker.Logic.Providers;

public sealed class SearchProviderFactory(List<SearchProviderDescriptor> providers)
{
    private readonly List<SearchProviderDescriptor> _providers = providers;
    public IReadOnlyList<SearchProviderDescriptor> Providers => _providers;
    public List<string> ProvidersNames => _providers.Select(x => x.Name).ToList();

    public static SearchProviderFactory Build(List<SearchProviderDescriptor> providers)
        => new(providers);

    public ISearchProvider InstantiateFromName(string name, string search_term)
    {
        SearchProviderDescriptor entry = Providers.Where(x => x.Name == name).FirstOrDefault()
            ?? throw new Exception("No valid provider found.");

        MethodInfo create_method = entry.ProviderType.GetMethod(nameof(ISearchProvider.Create), BindingFlags.Public | BindingFlags.Static)
            ?? throw new Exception("Method can not be loaded.");

        return create_method.Invoke(null, [search_term]) is not ISearchProvider provider
            ? throw new Exception("Failed to instantiate search provider.")
            : provider;
    }
}
