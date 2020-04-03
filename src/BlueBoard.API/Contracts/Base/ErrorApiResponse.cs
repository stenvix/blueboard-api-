using BlueBoard.Common.Enums;

namespace BlueBoard.API.Contracts.Base
{
    public class ErrorApiResponse : ApiResponse
    {
        protected ErrorApiResponse(ResponseCode responseCode, string responseMessage)
        {
            this.ResponseCode = responseCode;
            this.ResponseMessage = responseMessage;
        }

        public ResponseCode ResponseCode { get; }
        public string ResponseMessage { get; }
    }
}
