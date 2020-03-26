using BlueBoard.Common.Enums;

namespace BlueBoard.API.Contracts.Base
{
    public abstract class ApiResponse
    {
        protected ApiResponse(ResponseCode responseCode, string responseMessage)
        {
            this.ResponseCode = responseCode;
            this.ResponseMessage = responseMessage;
        }

        public ResponseCode ResponseCode { get; }
        public string ResponseMessage { get; }
    }
}
