using System;
using BlueBoard.Common.Enums;

namespace BlueBoard.Common
{
    public class BlueBoardException : Exception
    {
        public BlueBoardException(ResponseCode responseCode, string message = null, Exception innerException = null) :
            base(message, innerException)
        {
            this.ResponseCode = responseCode;
        }

        public ResponseCode ResponseCode { get; }
    }
}