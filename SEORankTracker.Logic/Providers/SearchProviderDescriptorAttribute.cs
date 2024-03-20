namespace SEORankTracker.Logic.Providers;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class SearchProviderDescriptorAttribute(string name) : Attribute
{
    public string Name { get; } = name;
}
