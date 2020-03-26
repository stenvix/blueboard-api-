using System;
using System.Collections.Generic;
using BlueBoard.Common;
using BlueBoard.Common.Enums;

namespace BlueBoard.Module.Common.Exceptions
{
    public class BlueBoardValidationException : BlueBoardException
    {
        public IDictionary<string, string[]> Errors { get; }

        public BlueBoardValidationException(IDictionary<string, string[]> errors) : base(ResponseCode.ValidationError)
        {
            this.Errors = errors;
        }
    }
}
