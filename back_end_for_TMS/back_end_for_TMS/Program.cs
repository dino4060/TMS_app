using back_end_for_TMS.Infrastructure.Api;
using back_end_for_TMS.Infrastructure.Business;
using back_end_for_TMS.Infrastructure.Database;
using back_end_for_TMS.Infrastructure.Security;

// Add services to the container.

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBusinessServices(builder.Configuration);

builder.Services.AddDatabaseServices(builder.Configuration);

builder.Services.AddApiServices(builder.Configuration);

builder.Services.AddSecurityServices(builder.Configuration);

// Configure the HTTP request pipeline.

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "Application is ready").AllowAnonymous();

await app.CheckDatabaseConnection();

app.Run();
