using System;
using System.ComponentModel;
using Grpc.Net.Client;
using GrpcApp.Common;
using ProtoBuf.Grpc.Client;
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
                    using (var channel = GrpcChannel.ForAddress("http://localhost:50001"))
                    {
                        var login_rpc = channel.CreateGrpcService<IGreeterService>();
                        var result = await login_rpc.SayHello(new HelloRequest() {Name = Guid.NewGuid().ToString() });
                        Console.WriteLine(result.Text);
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
