using Azure.Messaging.ServiceBus;
using TP.AsyncAPI.Data;
using TP.AsyncAPI.Logic;

public static class ServiceCollectionExtentions
{
    public static void AddRequiredServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddSingleton(configuration);
        services.AddSingleton(new ServiceBusClient(configuration["SBConnString"], new ServiceBusClientOptions{
            TransportType = ServiceBusTransportType.AmqpTcp
        }));
        services.AddSingleton(provider => provider
            .GetRequiredService<ServiceBusClient>()
            .CreateSender(configuration["SBRequestQueueName"]));
        services.AddSingleton(provider => provider
            .GetRequiredService<ServiceBusClient>()
            .CreateProcessor(configuration["SBRequestQueueName"]));
        services.AddSingleton<IProcessorSimulationLogic, ProcessorSimulationLogic>();
        services.AddSingleton<IProcessorSimulationService, ProcessSimulationService>();
        services.AddHostedService<AMQPProcessor>();
        services.AddMemoryCache();
    }
}
