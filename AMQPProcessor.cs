using Azure.Messaging.ServiceBus;
using System.Text.Json;
using TP.AsyncAPI.Logic;

public class AMQPProcessor : BackgroundService
{
    private readonly ServiceBusProcessor _processor;
    private readonly ServiceBusClient _client;
    private readonly IProcessorSimulationLogic _logic;
    private readonly ILogger<AMQPProcessor> _logger;

    public AMQPProcessor(
        ServiceBusProcessor processor,
        ServiceBusClient client,
        IProcessorSimulationLogic logic,
        ILogger<AMQPProcessor> logger
    )
    {
        _processor = processor ?? 
            throw new ArgumentNullException(nameof(processor));
        _client = client ?? 
            throw new ArgumentNullException(nameof(client));
        _logic = logic ?? 
            throw new ArgumentNullException(nameof(logic));
        _logger = logger ?? 
            throw new ArgumentNullException(nameof(logger));
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _processor.ProcessMessageAsync += async r => {
            _logger.LogInformation($"Received request: {r.Message.Body}");
            try
            {
                var requestData = JsonSerializer.Deserialize<RequestModel>(r.Message.Body);
                var responseData = await ProcessSimulationRequest(requestData);
                var responseMessage = new ServiceBusMessage(JsonSerializer.Serialize(responseData))
                {
                    CorrelationId = r.Message.CorrelationId
                };
                //Forward to another queue for further processing
                _ = _client.CreateSender(r.Message.ReplyTo).SendMessageAsync(responseMessage);
            }
            catch (Exception ex) 
            {
                await r.DeadLetterMessageAsync(
                    r.Message, 
                    ex.GetType().ToString(), 
                    ex.Message
                );
            }
        };

        _processor.ProcessErrorAsync += e => {
            _logger.LogError(e.Exception, $"Error processing message: {e.EntityPath}, {e.Identifier}");
            return Task.CompletedTask;
        };

        await _processor.StartProcessingAsync(stoppingToken);
    }

    private async Task<DataModel> ProcessSimulationRequest(RequestModel request)
    {
        return await _logic.ExecuteAsync(request);
    }
}
