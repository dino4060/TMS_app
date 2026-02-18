namespace back_end_for_TMS.Infrastructure.Database;

public static class DatabaseExtensions
{
    public static async Task CheckDatabaseConnection(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<AppDbContext>();
        var logger = services.GetRequiredService<ILogger<AppDbContext>>();

        try
        {
            if (await context.Database.CanConnectAsync())
            {
                logger.LogInformation(">>> DATABASE CONNECTION: SUCCESS <<<");
            }
            else
            {
                logger.LogWarning(">>> DATABASE CONNECTION: CANNOT CONNECT <<<");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(">>> DATABASE CONNECTION: FAILED! <<< \n{Message}", ex.Message);
        }
    }
}
