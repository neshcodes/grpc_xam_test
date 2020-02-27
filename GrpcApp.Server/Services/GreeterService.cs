using System.Threading.Tasks;
using GrpcApp.Common;
using Microsoft.Extensions.Logging;

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
        public async ValueTask<HelloReply> SayHello(HelloRequest request)
        {
            return new HelloReply
            {
                Text = "Hello " + request.Name
            };
        }
    }
}
