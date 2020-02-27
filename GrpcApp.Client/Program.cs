using System;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcApp.Common;
using ProtoBuf.Grpc.Client;

namespace GrpcApp.Client
{
    class Program
    {
        static async Task Main()
        {
            try
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
            }
        }
    }
}
