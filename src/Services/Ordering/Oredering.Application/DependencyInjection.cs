using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Oredering.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        //services.AddMediatR(cfg =>
        //{
        //    cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
        //});

        return services;
    }
 }
