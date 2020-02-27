using System;

namespace GrpcApp.Common.Msg
{
    public class RegisterPhonePassword_Request
    {
        public string phone { get; set; }

        public string password { get; set; }
    }

    public enum RegisterPhonePasswordResult : byte
    {
        None = 0,
        Success = 1,
        Exist = 2,

    }
    public class RegisterPhonePassword_Response
    {
        public RegisterPhonePasswordResult result { get; set; }
    }
}
