namespace TP.AsyncAPI.Data;

public interface IProcessorSimulationService
{
    Task<DataModel> CallDownStreamSystemAsync (RequestModel request);
    Task<DataModel?> GetStatusFromDatabaseAsync (string id);
}
