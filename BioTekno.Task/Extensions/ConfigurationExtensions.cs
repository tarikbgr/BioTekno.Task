using BioTekno.Task.Business;
using BioTekno.Task.Middleware;
using BioTekno.Task.Models.Input;
using BioTekno.Task.Models.Message;
using BioTekno.Task.Repositories.Abstract;
using BioTekno.Task.Repositories.Concrete;
using BioTekno.Task.Services;
using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace BioTekno.Task.Extensions;

public static class ConfigurationExtensions
{
    public static void ConfigureComponents(this IServiceCollection services)
    {
        services.AddScoped<IProductRepositoryAsync, ProductRepositoryAsync>();
        services.AddScoped<IOrderRepositoryAsync, OrderRepositoryAsync>();
        services.AddScoped<IOrderDetailRepositoryAsync, OrderDetailRepositoryAsync>();
        services.AddScoped<IProductBusiness, ProductBusiness>();
        services.AddScoped<IOrderBusiness, OrderBusiness>();

        services.AddScoped(typeof(IRedisCacheService<>), typeof(RedisCacheService<>));

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateOrderRequest>());
    }

    public static void ConfigureRedis(this IServiceCollection services, IConfiguration configuration)
    {
        var multiplexer = ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis"));
        services.AddSingleton<IConnectionMultiplexer>(multiplexer);
    }

    public static void ConfigureSql(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<Context>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
        );
    }

    public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<GlobalErrorHandlingMiddleware>();
    }

    public static void ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.GetSection("MassTransit").Get<SettingMassTransit>();

        services.AddMassTransit(
            mt =>
            {
                mt.UsingRabbitMq(
                    (context, cfg) =>
                    {
                        cfg.Host(
                            settings.Host,
                            settings.Port,
                            settings.VirtualHost,
                            settings.ConnectionName,
                            h =>
                            {
                                h.Username(settings.Username);
                                h.Password(settings.Password);
                            }
                        );
                    }
                );
            }
        );
        services.AddMassTransitHostedService();
    }
}