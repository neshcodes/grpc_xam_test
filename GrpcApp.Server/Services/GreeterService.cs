using System.ServiceModel;
using System.Threading.Tasks;
using GrpcApp.Common;
using Microsoft.Extensions.Logging;
using ProtoBuf.Grpc.Configuration;

namespace GrpcApp.Server.Services
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

        [Operation]
        public async ValueTask<HelloReply> SayHello(HelloRequest request)
        {
            return new HelloReply
            {
                Text = "Hello " + request.Name
            };
        }
    }
}
