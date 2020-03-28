using System;
using System.Collections.Generic;
using System.Linq;
using BlueBoard.Common;
using BlueBoard.Common.Enums;

namespace BlueBoard.Module.Common.Exceptions
{
    public class BlueBoardValidationException : BlueBoardException
    {
        public BlueBoardValidationException(string error) : base(ResponseCode.ValidationError)
        {
            this.Failures = new Dictionary<string, string[]> {{error, new[] {error}}};
            this.SetErrors();
        }

        public BlueBoardValidationException(IDictionary<string, string[]> failures) : base(ResponseCode.ValidationError)
        {
            this.Failures = failures;
            this.SetErrors();
        }

        public IDictionary<string, string[]> Failures { get; }

        public IList<string> Errors { get; private set; }

        private void SetErrors()
        {
            this.Errors = this.Failures.SelectMany(i => i.Value).Distinct().ToList();
        }
    }
}
