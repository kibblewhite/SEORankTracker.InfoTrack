# SEO Rank Tracker

### Connection String

The connection string can be found in the `appsettings.Development.json` file under the `SEORankTracker.App.Server` project:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SEORankTracker;Trusted_Connection=True;MultipleActiveResultSets=true;"
}
```


### Initialising the Database

After configuring the connection string correctly, the `SEORankTracker.App.Server` application provides an accessible endpoint via Swagger.

This endpoint allows the invocation of `/create-database` to initialise the database.

This is a prerequisite before running the `SEORankTracker.App.Client` application.


### Running the Application(s)

To ensure proper operation, the `SEORankTracker.App.Server` application should be running before launching `SEORankTracker.App.Client`.

The server hosts Swagger, while the client is a Blazor application.

Note: The server should be running on `http://localhost:5142`
