using Microsoft.Extensions.Caching.Memory;

namespace TP.AsyncAPI.Data;

public class ProcessSimulationService: IProcessorSimulationService
{
    private readonly IMemoryCache _memoryCache;
    private int CACHE_MINUTES = 60;

    public ProcessSimulationService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache ??
            throw new ArgumentNullException(nameof(memoryCache));
    }

    public async Task<DataModel> CallDownStreamSystemAsync(RequestModel request)
    {
        await Task.Delay(1000);

        var data = new DataModel {
            Id = request.Id,
            Message = request.Message,
            CorrelationId = request.CorrelationId,
            DateTimeRequest = request.DateTimeRequest,
            DateTimeResponse = DateTimeOffset.Now
        };
        
        _memoryCache.Set(
            data.CorrelationId,
            data,
            TimeSpan.FromMinutes(CACHE_MINUTES)
        );

        return data;
    }

    public Task<DataModel?> GetStatusFromDatabaseAsync(string id)
    {
        return Task.FromResult(_memoryCache.Get<DataModel>(id));
    }
}
