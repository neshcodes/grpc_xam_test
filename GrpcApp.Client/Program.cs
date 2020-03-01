using System;
using System.IO;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcApp.Common;
using ProtoBuf.Grpc.Client;
using ProtoBuf.Grpc.Configuration;

namespace GrpcApp.Client
{
    public static class GrpcExtensions
    {
        public static Task<TResponse> Execute<TRequest, TResponse>(this GrpcChannel channel, TRequest request, string serviceName, string methodName,
            CallOptions options = default, string? host = null)
            where TRequest : class
            where TResponse : class
            => Execute<TRequest, TResponse>(channel.CreateCallInvoker(), request, serviceName, methodName, options, host);

        public static async Task<TResponse> Execute<TRequest, TResponse>(this CallInvoker invoker, TRequest request, string serviceName, string methodName,
            CallOptions options = default, string? host = null)
            where TRequest : class
            where TResponse : class
        {
            var method = new Method<TRequest, TResponse>(MethodType.Unary, serviceName, methodName,
                CustomMarshaller<TRequest>.Instance, CustomMarshaller<TResponse>.Instance);
            using (var auc = invoker.AsyncUnaryCall(method, host, options, request))
            {
                return await auc.ResponseAsync;
            }
        }

        class CustomMarshaller<T> : Marshaller<T>
        {
            public static readonly CustomMarshaller<T> Instance = new CustomMarshaller<T>();
            private CustomMarshaller() : base(Serialize, Deserialize) { }

            private static T Deserialize(byte[] payload)
            {
                using (var ms = new MemoryStream(payload))
                {
                    return ProtoBuf.Serializer.Deserialize<T>(ms);
                }
            }
            private static byte[] Serialize(T payload)
            {
                using (var ms = new MemoryStream())
                {
                    ProtoBuf.Serializer.Serialize<T>(ms, payload);
                    return ms.ToArray();
                }
            }
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
                    var channel = new Channel("localhost", 12345, ChannelCredentials.Insecure);
                    //using (var channel = GrpcChannel.ForAddress("https://localhost:5001"))
                    //using (var channel = GrpcChannel.ForAddress("https://localhost:5001"))
                    {
                        HelloRequest request = new HelloRequest();
                        request.Name = "11111";
                        var client = new GrpcClient(channel, "Greeter");
                        var response = await client.UnaryAsync<HelloRequest, HelloReply>(request, nameof(IGreeterService.SayHello));
                        Console.WriteLine(response.Text);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
