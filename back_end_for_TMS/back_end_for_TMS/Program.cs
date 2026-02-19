using back_end_for_TMS.Infrastructure.Database;
using back_end_for_TMS.Infrastructure.DependencyInjection;
using back_end_for_TMS.Infrastructure.Security;

// Add services to the container.

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddSecurityServices(builder.Configuration);

builder.Services.AddApplicationServices(builder.Configuration);

// Configure the HTTP request pipeline.

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "Application is ready").AllowAnonymous();

await app.CheckDatabaseConnection();

app.Run();
