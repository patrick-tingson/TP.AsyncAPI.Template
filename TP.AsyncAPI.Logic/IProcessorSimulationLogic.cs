namespace TP.AsyncAPI.Logic;

public interface IProcessorSimulationLogic
{
    Task<DataModel> ExecuteAsync (RequestModel request);
    Task<DataModel> InquireStatusAsync (string correlationId);
}
