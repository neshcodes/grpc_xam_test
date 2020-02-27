using System.ServiceModel;
using System.Threading.Tasks;
using GrpcApp.Common.Msg;

namespace GrpcApp.Common.Service
{
    [ServiceContract]
    public interface ILoginService
    {
        //the function cant set same name
        ValueTask<LoginResponse> LoginPassword(LoginPasswordRequest request);

        ValueTask<LoginResponse> LoginVerifyCode(LoginVerifyCodeRequest request);
    }
}
