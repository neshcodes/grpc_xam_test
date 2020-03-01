using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using Grpc.Core;
using GrpcApp.Common;
using ProtoBuf.Grpc.Client;
using ProtoBuf.Grpc.Configuration;
using Xamarin.Forms;

namespace GrpcApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            Xamarin.Essentials.MainThread.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    GrpcClientFactory.AllowUnencryptedHttp2 = true;
                    var channel = new Channel("10.0.2.2", 50001, ChannelCredentials.Insecure);
                    //using (var channel = GrpcChannel.ForAddress("http://127.0.0.1:50001"))
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

                /*try
                {
                    GrpcClientFactory.AllowUnencryptedHttp2 = true;
                    using (var channel = GrpcChannel.ForAddress("http://10.0.2.2:50001"))
                    {
                        var serviceType = typeof(IGreeterService);
                        ServiceBinder.Default.IsServiceContract(serviceType, out var serviceName);


                        var method = serviceType.GetMethod(nameof(IGreeterService.SayHello))!;
                        ServiceBinder.Default.IsOperationContract(method, out var operationName);

                        HelloRequest request = new HelloRequest() { Name = Guid.NewGuid().ToString() };
                        var response = await channel.Execute<HelloRequest, HelloReply>(request, "GrpcApp.Common.GreeterService", "SayHello");
                        Console.WriteLine(response.Text);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }*/
            });
        }
    }
}
