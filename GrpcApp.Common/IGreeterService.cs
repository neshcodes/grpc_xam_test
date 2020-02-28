using System.Runtime.Serialization;
using System.ServiceModel;
using System.Threading.Tasks;
using ProtoBuf;

namespace GrpcApp.Common
{
    [ServiceContract, ProtoContract]
    public interface IGreeterService
    {
        ValueTask<HelloReply> SayHello(HelloRequest request);
    }

    [DataContract, ProtoContract]
    public class HelloReply
    {
        [DataMember(Order = 1), ProtoMember(1)]
        public string Text { get; set; }
    }

    [DataContract, ProtoContract]
    public class HelloRequest
    {
        [DataMember(Order = 1), ProtoMember(1)]
        public string Name { get; set; }
    }
}
