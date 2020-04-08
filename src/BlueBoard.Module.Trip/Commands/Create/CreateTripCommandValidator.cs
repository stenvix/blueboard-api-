using System;
using BlueBoard.Contract.Common;
using BlueBoard.Contract.Trip.Commands;
using BlueBoard.Persistence.Abstractions;
using FluentValidation;
using FluentValidation.Validators;

namespace BlueBoard.Module.Trip.Commands.Create
{
    public class CreateTripCommandValidator : AbstractValidator<CreateTripCommand>
    {
        public CreateTripCommandValidator()
        {
            this.RuleFor(i => i.Trip)
                .NotNull().WithErrorCode(ErrorCodes.EmptyTrip);

            this.RuleFor(i => i.Trip.Name)
                .NotEmpty().WithErrorCode(ErrorCodes.EmptyName)
                .SetValidator(new MaximumLengthValidator(Constraints.NameLength))
                .WithErrorCode(ErrorCodes.InvalidNameLength);

            this.RuleFor(i => i.Trip.Description)
                .SetValidator(new MaximumLengthValidator(Constraints.DescriptionLength))
                .WithErrorCode(ErrorCodes.InvalidDescriptionLength)
                .When(i => !string.IsNullOrEmpty(i.Trip.Description));

            this.RuleFor(i => i.Trip.StartDate)
                .NotEmpty().WithErrorCode(ErrorCodes.EmptyStartDate)
                .GreaterThan(DateTime.UtcNow.Date).WithErrorCode(ErrorCodes.InvalidStartDate);

            this.RuleFor(i => i.Trip.EndDate)
                .NotEmpty().WithErrorCode(ErrorCodes.EmptyEndDate)
                .GreaterThan(command => command.Trip.StartDate).WithErrorCode(ErrorCodes.InvalidEndDate);
        }
    }
}
