// See https://aka.ms/new-console-template for more information

using Grpc.Net.Client;
using GrpcClient;

using var channel = GrpcChannel.ForAddress("http://localhost:9090");
var client = new Greeter.GreeterClient(channel);
var reply = await client.sayHelloAsync(
    new HelloRequest { Name = "GreeterClient" });
Console.WriteLine("Greeting: " + reply.Message);
Console.WriteLine("Press any key to exit...");
Console.ReadKey();