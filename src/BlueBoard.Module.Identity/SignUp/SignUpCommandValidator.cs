using BlueBoard.Contract.Common;
using BlueBoard.Contract.Identity.Commands;
using FluentValidation;

namespace BlueBoard.Module.Identity.SignUp
{
    public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
    {
        public SignUpCommandValidator()
        {
            this.RuleFor(i => i.Email)
                .NotEmpty().WithErrorCode(ErrorCodes.EmptyEmail)
                .EmailAddress().WithErrorCode(ErrorCodes.InvalidEmail);
        }
    }
}
