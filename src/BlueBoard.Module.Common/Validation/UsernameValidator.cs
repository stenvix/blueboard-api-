using System.Text.RegularExpressions;
using BlueBoard.Contract.Common;
using BlueBoard.Persistence.Abstractions;
using FluentValidation;
using FluentValidation.Validators;

namespace BlueBoard.Module.Common.Validation
{
    public class UsernameValidator : AbstractValidator<string>
    {
        private readonly Regex usernameRegex = new Regex(@"^[a-z0-9_.-]*$");

        public UsernameValidator()
        {
            this.RuleFor(i => i)
                .Must(i => this.usernameRegex.IsMatch(i))
                .WithErrorCode(ErrorCodes.InvalidUsername)
                .SetValidator(new MaximumLengthValidator(Constraints.NameLength))
                .WithErrorCode(ErrorCodes.InvalidUsernameLength);
        }
    }
}
