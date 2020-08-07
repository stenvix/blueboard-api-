using BlueBoard.Contract.Common;
using FluentValidation;

namespace BlueBoard.Module.Identity.Validators
{
    public class SignInValidator : AbstractValidator<Contract.Identity.Commands.SignIn>
    {
        public SignInValidator()
        {
            this.RuleFor(i => i.Email)
                .NotEmpty().WithErrorCode(ErrorCodes.EmptyEmail)
                .EmailAddress().WithErrorCode(ErrorCodes.InvalidEmail);
        }
    }
}
