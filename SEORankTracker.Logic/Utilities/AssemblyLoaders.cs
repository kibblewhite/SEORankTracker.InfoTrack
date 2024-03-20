using System.Collections.Generic;

namespace SEORankTracker.Logic.Utilities;

public static class AssemblyLoaders
{
    private static IEnumerable<Type> LoadHandlerTypesFromAssembly(this Assembly assembly)
        => assembly.GetTypes()
            .Where(t => !t.IsInterface)
            .Where(t => t.GetInterfaces().Any(i => i.IsGenericType
            && (typeof(IAsyncDialogueHandler<,>) == i.GetGenericTypeDefinition()
            || typeof(IAsyncRequestHandler<>) == i.GetGenericTypeDefinition())));

    public static IServiceCollection AddAsyncRequestHandlers(this IServiceCollection services)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        IEnumerable<Type> handler_types = assembly.LoadHandlerTypesFromAssembly();
        foreach (Type gen in handler_types)
        {
            services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining(gen));
        }

        return services;
    }

    private static IEnumerable<Type> LoadHttpRequestTypes(this Assembly assembly)
         => assembly.GetTypes()
             .Where(t => !t.IsInterface)
             .Where(t => t.GetInterfaces().Any(i => i.IsGenericType
             && (typeof(IHttpDialogue<>) == i.GetGenericTypeDefinition()
             || typeof(IHttpRequest) == i.GetGenericTypeDefinition())));

    private static IEnumerable<Type> LoadValidatorTypesFromAssembly(this Assembly assembly)
         => assembly.GetTypes()
             .Where(t => !t.IsInterface)
             .Where(t => t.GetInterfaces().Any(i => i.IsGenericType
             && typeof(IValidator<>) == i.GetGenericTypeDefinition()));

    public static IServiceCollection AddFluentValidators(this IServiceCollection services)
    {
        Assembly assembly = AssemblyUtility.GetSharedAssembly();

        // Load all posible requests that inherit from `IHttpDialogue<>` or `IHttpRequest`
        IEnumerable<Type> http_request_types = assembly.LoadHttpRequestTypes();
        foreach (Type http_request_type in http_request_types)
        {
            Type[] generic_type_arguments = [http_request_type];
            Type request_preprocessor_type = typeof(IRequestPreProcessor<>);
            Type preprocessor_type = typeof(ValidationPreProcessor<>);

            Type constructed_request_preprocessor_type = request_preprocessor_type.MakeGenericType(generic_type_arguments);
            Type constructed_preprocessor_type = preprocessor_type.MakeGenericType(generic_type_arguments);

            services.AddTransient(constructed_request_preprocessor_type, constructed_preprocessor_type);
            services.AddMediatR(x => {
                x.RegisterServicesFromAssemblyContaining(constructed_request_preprocessor_type);
                x.AddRequestPreProcessor(constructed_request_preprocessor_type, constructed_preprocessor_type);
            });
        }

        // Load all posible validators that inherit from `IValidator<>`
        IEnumerable<Type> validator_types = assembly.LoadValidatorTypesFromAssembly();
        foreach (Type implemented_validator_type in validator_types)
        {
            Type? base_type = implemented_validator_type.GetInterfaces().FirstOrDefault(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IValidator<>));
            Type? generic_type_argument = base_type?.GetGenericArguments().FirstOrDefault();
            if (generic_type_argument is null)
            {
                continue;
            }

            Type validator_type = typeof(IValidator<>);
            Type constructed_validator_type = validator_type.MakeGenericType(generic_type_argument);
            services.AddTransient(constructed_validator_type, implemented_validator_type);
        }

        return services;
    }

    private static IEnumerable<Type> LoadProviderTypesFromAssembly(this Assembly assembly)
        => assembly.GetTypes()
            .Where(t => typeof(ISearchProvider).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

    public static IServiceCollection AddSearchProviderFactory(this IServiceCollection services)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        IEnumerable<Type> provider_types = assembly.LoadProviderTypesFromAssembly();

        List<SearchProviderDescriptor> search_provider_descriptors = [];
        foreach (Type provider_type in provider_types)
        {
            if (Attribute.IsDefined(provider_type, typeof(SearchProviderDescriptorAttribute)) is false)
            {
                continue;
            }

            if (Attribute.GetCustomAttribute(provider_type, typeof(SearchProviderDescriptorAttribute)) is not SearchProviderDescriptorAttribute attribute)
            {
                continue;
            }

            search_provider_descriptors.Add(new SearchProviderDescriptor
            {
                Name = attribute.Name,
                ProviderType = provider_type
            });
        }

        SearchProviderFactory search_provider_factory = SearchProviderFactory.Build(search_provider_descriptors);
        services.AddSingleton(search_provider_factory);
        return services;
    }
}
