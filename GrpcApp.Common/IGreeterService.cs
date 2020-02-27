using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Threading.Tasks;

namespace GrpcApp.Common
{
    [ServiceContract]
    public interface IGreeterService
    {
        ValueTask<HelloReply> SayHello(HelloRequest request);
    }

    [DataContract]
    public class HelloReply
    {
        [DataMember(Order = 1)]
        public string Text { get; set; }
    }

    public class HelloRequest
    {
        [DataMember(Order = 1)]
        public string Name { get; set; }
    }
}
