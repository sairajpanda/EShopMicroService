using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MassTransit;
using MassTransit.RabbitMqTransport;

namespace BuildingBlocks.Messaging.MassTransit;

public static class Extensions
{
    public static IServiceCollection AddMessageBroker
        (this IServiceCollection services,IConfiguration configuration, Assembly? assembly = null)
    {
        services.AddMassTransit(config =>
        {
            config.SetKebabCaseEndpointNameFormatter();

            if (assembly != null) config.AddConsumers(assembly);

            config.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(configuration["MessageBroker:HostName"]!, h =>
                {
                    h.Username(configuration["MessageBroker:UserName"]!);
                    h.Password(configuration["MessageBroker:Password"]!);
                });
                configurator.ConfigureEndpoints(context);
            });
            if (assembly is not null)
            {
                config.AddConsumers(assembly);
            }
        });

        return services;
    }
}
