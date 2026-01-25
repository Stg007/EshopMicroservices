using Microsoft.EntityFrameworkCore.Diagnostics;
using Oredering.Infrastructure.Data.Interceptors;

namespace Oredering.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices
        (this IServiceCollection services, IConfiguration configuration)
    {
        // Register infrastructure services here

        var connectionString = configuration.GetConnectionString("Database");
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp,options) =>
        {
            options.UseSqlServer(connectionString);
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
        });

        //services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        return services;
    }
}
