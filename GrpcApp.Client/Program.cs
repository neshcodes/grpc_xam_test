using System;
using System.IO;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcApp.Common;
using ProtoBuf.Grpc.Client;
using ProtoBuf.Grpc.Configuration;
using ProtoBuf.Grpc.Client;
using ProtoBuf.Grpc.Configuration;
using ProtoBuf.Grpc.Server;

namespace GrpcApp.Client
{
    public class GreeterService : IGreeterService
    {
        /*public ValueTask<HelloReply> SayHello(HelloRequest request)
        {
            return new ValueTask<HelloReply>(new HelloReply
            {
                Text = "Hello " + request.Name
            });
        }*/

        public async ValueTask<HelloReply> SayHello(HelloRequest request)
        {
            return new HelloReply
            {
                Text = "Hello " + request.Name
            };
        }
    }

    class Program
    {

        static async Task Main()
        {
            try
            {
                /*GrpcClientFactory.AllowUnencryptedHttp2 = true;
                using (var channel = GrpcChannel.ForAddress("http://localhost:50001"))
                {
                    var serviceType = typeof(IGreeterService);
                    ServiceBinder.Default.IsServiceContract(serviceType, out var serviceName);
                    

                    var method = serviceType.GetMethod(nameof(IGreeterService.SayHello))!;
                    ServiceBinder.Default.IsOperationContract(method, out var operationName);

                    HelloRequest request = new HelloRequest() { Name = Guid.NewGuid().ToString() };
                    var response = await channel.Execute<HelloRequest, HelloReply>(request, "GrpcApp.Common.GreeterService", "SayHello");
                    Console.WriteLine(response.Text);
                }*/

                /*try
                {
                    GrpcClientFactory.AllowUnencryptedHttp2 = true;
                    using (var channel = GrpcChannel.ForAddress("http://localhost:50001"))
                    {
                        var login_rpc = channel.CreateGrpcService<IGreeterService>();
                        var result = await login_rpc.SayHello(new HelloRequest() { Name = Guid.NewGuid().ToString() });
                        Console.WriteLine(result.Text);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }*/

                try
                {
                    GrpcClientFactory.AllowUnencryptedHttp2 = true;
                    var channel = new Channel("localhost", 50001, ChannelCredentials.Insecure);
                    {
                        HelloRequest request = new HelloRequest();
                        request.Name = "11111";
                        var client = new GrpcClient(channel, typeof(IGreeterService));
                        var response = await client.UnaryAsync<HelloRequest, HelloReply>(request, nameof(IGreeterService.SayHello));
                        Console.WriteLine(response.Text);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                /*Server server = new Server
                {
                    Ports = { new ServerPort("localhost", 50051, ServerCredentials.Insecure) }
                };
                server.Services.AddCodeFirst(new GreeterService());
                server.Start();

                GrpcClientFactory.AllowUnencryptedHttp2 = true;
                Channel channel = new Channel("localhost:50051", ChannelCredentials.Insecure);

                //GrpcClientFactory.AllowUnencryptedHttp2 = true;
                //var channel = new Channel("10.0.2.2", 50001, ChannelCredentials.Insecure);
                //using (var channel = GrpcChannel.ForAddress("http://127.0.0.1:50001"))
                //using (var channel = GrpcChannel.ForAddress("https://localhost:5001"))
                {
                    HelloRequest request = new HelloRequest();
                    request.Name = "11111";
                    var client = new GrpcClient(channel, "Greeter");
                    var response = await client.UnaryAsync<HelloRequest, HelloReply>(request, "SayHello");
                    Console.WriteLine(response.Text);
                }*/
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.ReadLine();
        }
    }
}
