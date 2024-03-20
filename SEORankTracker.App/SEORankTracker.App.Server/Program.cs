namespace SEORankTracker.App.Server;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCors(options
            => options.AddDefaultPolicy(builder
            => builder.SetIsOriginAllowed(origin => true).AllowAnyMethod().AllowAnyHeader().AllowCredentials()));

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddProblemDetails();

        builder.Services.AddDbContext<ApplicationDatabaseContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddAsyncRequestHandlers();
        builder.Services.AddFluentValidators();
        builder.Services.AddSearchProviderFactory();

        WebApplication app = builder.Build();

        app.UseCors();
        app.UseSwagger();
        app.UseSwaggerUI();

        app.MediatePost<EnsureDatabaseCreatedRequest, EnsureDatabaseCreatedResponse>("/create-database");
        app.MediateGet<ProvidersRequest, ProvidersResponse>("/providers");
        app.MediateGet<HistoryRequest, HistoryResponse>("/history");
        app.MediatePost<SearchRequest, SearchResponse>("/search");

        app.Run();
    }
}
