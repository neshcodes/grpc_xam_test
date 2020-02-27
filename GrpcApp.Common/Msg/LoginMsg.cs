using System.Runtime.Serialization;

namespace GrpcApp.Common.Msg
{
    [DataContract]
    public enum LoginResult
    {
        [EnumMember]
        None = 0,
        [EnumMember]
        Success = 1,
        [EnumMember]
        UnExist = 2,
        [EnumMember]
        ErrorPWD = 3,
    }

    [DataContract]
    public class LoginResponse
    {
        [DataMember(Order = 1)]
        public LoginResult Result { get; set; }
    }

    [DataContract]
    public class LoginPasswordRequest
    {
        [DataMember(Order = 1)]
        public string Phone { get; set; }

        [DataMember(Order = 2)]
        public string Password { get; set; }
    }

    [DataContract]
    public class LoginVerifyCodeRequest
    {
        [DataMember(Order = 1)]
        public string Phone { get; set; }

        [DataMember(Order = 2)]
        public string VerifyCode { get; set; }
    }
}
