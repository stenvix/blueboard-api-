using BlueBoard.API.Contracts.Base;
using BlueBoard.Common.Enums;

namespace BlueBoard.API.Contracts.Auth
{
    public class SignInResponse : ApiResponse
    {
        public SignInResponse(ResponseCode responseCode, string responseMessage = null) : base(responseCode, responseMessage)
        {
        }
    }
}
