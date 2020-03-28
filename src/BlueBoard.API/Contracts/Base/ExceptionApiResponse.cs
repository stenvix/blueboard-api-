using System.Collections.Generic;
using BlueBoard.Common.Enums;

namespace BlueBoard.API.Contracts.Base
{
    public class ExceptionApiResponse : ApiResponse
    {
        public IList<string> Errors { get; }

        public ExceptionApiResponse(ResponseCode responseCode, IList<string> errors, string responseMessage = null) :
            base(responseCode, responseMessage)
        {
            this.Errors = errors;
        }
    }
}
