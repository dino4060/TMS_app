using back_end_for_TMS.Business;
using back_end_for_TMS.Infrastructure.Database;
using back_end_for_TMS.Infrastructure.Mapper;

namespace back_end_for_TMS.Infrastructure.DependencyInjection;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddAutoMapper(typeof(AppMapperProfile).Assembly);

        services.AddDbContext<AppDbContext>();

        services.AddScoped<TokenService>();

        services.AddScoped<AccountService>();

        return services;
    }
}
