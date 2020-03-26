using BlueBoard.Contract.Common;
using BlueBoard.Contract.Identity.Commands;
using FluentValidation;

namespace BlueBoard.Module.Identity.SignIn
{
    public class SignInCommandValidator : AbstractValidator<SignInCommand>
    {
        public SignInCommandValidator()
        {
            // this.RuleFor(i => i.Email)
            //     .NotEmpty().WithErrorCode(ErrorCodes.EmptyEmail)
            //     .EmailAddress().WithErrorCode(ErrorCodes.InvalidEmail);
        }
    }
}
