using System.Runtime.Serialization;
using System.ServiceModel;
using System.Threading.Tasks;
using ProtoBuf;
using ProtoBuf.Grpc.Configuration;

namespace GrpcApp.Common
{
    [Service]
    public interface IGreeterService
    {
        [Operation]
        ValueTask<HelloReply> SayHello(HelloRequest request);
    }

    [DataContract, ProtoContract]
    public class HelloReply
    {
        [DataMember(Order = 1), ProtoMember(1)]
        public string Text { get; set; }
    }

    [DataContract]
    public class HelloRequest
    {
        [DataMember(Order = 1), ProtoMember(1)]
        public string Name { get; set; }
    }
}
