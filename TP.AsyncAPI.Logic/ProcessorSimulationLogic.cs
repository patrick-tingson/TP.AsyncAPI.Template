using System.Text.Json;
using Azure.Messaging.ServiceBus;
using TP.AsyncAPI.Data;

namespace TP.AsyncAPI.Logic;

public class ProcessorSimulationLogic: IProcessorSimulationLogic
{
    private readonly IProcessorSimulationService _service;
    
    public ProcessorSimulationLogic(IProcessorSimulationService service) => 
        _service = service ?? throw new ArgumentNullException(nameof(service));

    public async Task<DataModel> ExecuteAsync(RequestModel request) => 
        await _service.CallDownStreamSystemAsync(request);

    public async Task<DataModel?> InquireStatusAsync(string correlationId) =>
        await _service.GetStatusFromDatabaseAsync(correlationId);
}
