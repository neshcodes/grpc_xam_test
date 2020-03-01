using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using Grpc.Core;
using GrpcApp.Common;
using ProtoBuf.Grpc.Client;
using ProtoBuf.Grpc.Configuration;
using ProtoBuf.Grpc.Server;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GrpcApp
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

    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        void SendButtonClicked(object sender, EventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    GrpcClientFactory.AllowUnencryptedHttp2 = true;
                    //Channel channel = new Channel("localhost:50051", ChannelCredentials.Insecure);

                    //GrpcClientFactory.AllowUnencryptedHttp2 = true;
                    var channel = new Channel("10.0.2.2", 50001, ChannelCredentials.Insecure);
                    //using (var channel = GrpcChannel.ForAddress("http://127.0.0.1:50001"))
                    //using (var channel = GrpcChannel.ForAddress("https://localhost:5001"))
                    {
                        HelloRequest request = new HelloRequest();
                        request.Name = Guid.NewGuid().ToString();
                        var client = new GrpcClient(channel, typeof(IGreeterService));
                        var response = await client.UnaryAsync<HelloRequest, HelloReply>(request, nameof(IGreeterService.SayHello));

                        this.SendButton.Text = response.Text;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            });
        }
    }
}
