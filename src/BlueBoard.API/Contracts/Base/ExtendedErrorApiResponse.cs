using System.Collections.Generic;
using BlueBoard.Common.Enums;

namespace BlueBoard.API.Contracts.Base
{
    public class ExtendedErrorApiResponse : ErrorApiResponse
    {
        public ExtendedErrorApiResponse(ResponseCode responseCode, IList<string> errors, string responseMessage = null) :
            base(responseCode, responseMessage)
        {
            this.Errors = errors;
        }

        public IList<string> Errors { get; }
    }
}
