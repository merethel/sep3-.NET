using Grpc.Core;
using Microsoft.Extensions.Logging;
using Shared.Models;

namespace GrpcClient.Services;

public class EventService : Greeter.GreeterClient
{
    
    private readonly ILogger<EventService> _logger;

    public EventService(ILogger<EventService> logger)
    {
        _logger = logger;
    }

    public override Task<HelloReply> sayHello(HelloRequest request, ServerCallContext context)
    {
        return Task.FromResult(new HelloReply
        {
            Message = "Hello " + request.Name
        });
    }
}