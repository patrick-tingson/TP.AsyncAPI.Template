using System.Linq;
using System.Net;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using TP.AsyncAPI.Logic;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRequiredServices(builder.Configuration);
var app = builder.Build();
app.AddRequiredBuilder(builder);

app.MapGet("api/v1/status", async (
    string id,
    HttpContext context,
    IProcessorSimulationLogic logic
) => {
    var data = await logic.InquireStatusAsync(id);
    
    if(data is null) {
        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
        return;
    }

    context.Response.StatusCode = (int)HttpStatusCode.OK;
    await context.Response.WriteAsJsonAsync(data);
});

app.MapPost("api/v1/request", async (
    HttpContext context,
    RequestViewModel rvm,
    ServiceBusClient client,
    ServiceBusSender sender
) => {

    RequestModel rm = new() {
        Id = rvm.Id,
        Message = rvm.Message,
        DateTimeRequest = DateTimeOffset.Now,
        CorrelationId = Guid.NewGuid().ToString()
    };
    
    ServiceBusMessage message = new(JsonSerializer.Serialize(rm))
    {
        CorrelationId = rm.CorrelationId,
        ReplyTo = "<QueueName>"
    };

    try
    {
        await sender.SendMessageAsync(message);

        context.Response.StatusCode = (int)HttpStatusCode.Accepted;
        await context.Response.WriteAsJsonAsync(rm);
    }
    catch (Exception ex)
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        await context.Response.WriteAsJsonAsync(new {
            ErrorCode = 888,
            ErrorMessage = ex.Message
        });
    }
});

app.Run();