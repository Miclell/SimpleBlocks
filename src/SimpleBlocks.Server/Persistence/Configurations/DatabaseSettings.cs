namespace SimpleBlocks.Server.Persistence.Configurations;

public static class DatabaseSettings
{
    public static string GetConnectionString(IConfiguration configuration)
    {
        var host = configuration["DB_HOST"];
        var port = configuration["DB_PORT"];
        var database = configuration["DB_NAME"];
        var user = configuration["DB_USER"];
        var password = configuration["DB_PASSWORD"];

        return $"Host={host};Port={port};Database={database};Username={user};Password={password}";
    }
}