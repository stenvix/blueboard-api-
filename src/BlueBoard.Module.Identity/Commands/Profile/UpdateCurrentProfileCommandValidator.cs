using BlueBoard.Contract.Common;
using BlueBoard.Contract.Identity.Commands;
using BlueBoard.Module.Common.Validation;
using BlueBoard.Persistence.Abstractions;
using FluentValidation;

namespace BlueBoard.Module.Identity.Commands.Profile
{
    public class UpdateCurrentProfileCommandValidator : AbstractValidator<UpdateCurrentProfileCommand>
    {
        public UpdateCurrentProfileCommandValidator()
        {
            this.RuleFor(i => i.Profile)
                .NotNull().WithErrorCode(ErrorCodes.EmptyProfile);

            this.ValidateEmail(i => i.Profile.Email, command => !string.IsNullOrEmpty(command.Profile.Email));

            this.RuleFor(i => i.Profile.FirstName)
                .MaximumLength(Constraints.NameLength)
                .WithErrorCode(ErrorCodes.InvalidFirstNameLength)
                .When(i => !string.IsNullOrEmpty(i.Profile.FirstName));

            this.RuleFor(i => i.Profile.LastName)
                .MaximumLength(Constraints.NameLength)
                .WithErrorCode(ErrorCodes.InvalidLastNameLength)
                .When(i => !string.IsNullOrEmpty(i.Profile.LastName));

            this.RuleFor(i => i.Profile.Username)
                .SetValidator(new UsernameValidator())
                .When(i => !string.IsNullOrEmpty(i.Profile.Username));

            this.RuleFor(i => i.Profile.Phone)
                .SetValidator(new PhoneValidator())
                .WithErrorCode(ErrorCodes.InvalidPhone)
                .MaximumLength(Constraints.PhoneLength)
                .WithErrorCode(ErrorCodes.InvalidPhoneLength)
                .When(i => !string.IsNullOrEmpty(i.Profile.Phone));
        }
    }
}
