using BlueBoard.Contract.Common;
using BlueBoard.Contract.Identity.Commands;
using FluentValidation;

namespace BlueBoard.Module.Identity.Validators
{
    public class SignUpValidator : AbstractValidator<SignUp>
    {
        public SignUpValidator()
        {
            this.RuleFor(i => i.Email)
                .NotEmpty().WithErrorCode(ErrorCodes.EmptyEmail)
                .EmailAddress().WithErrorCode(ErrorCodes.InvalidEmail);
        }
    }
}
