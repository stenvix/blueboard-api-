using BlueBoard.API.Contracts.Base;
using BlueBoard.Common.Enums;

namespace BlueBoard.API.Contracts.Auth
{
    public class SignUpResponse : ApiResponse
    {
        public SignUpResponse(ResponseCode responseCode, string responseMessage = null) : base(responseCode, responseMessage)
        {
        }
    }
}
