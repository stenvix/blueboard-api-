using System;
using BlueBoard.Contract.Common;
using BlueBoard.Contract.Trip.Commands;
using BlueBoard.Persistence.Abstractions;
using FluentValidation;
using FluentValidation.Validators;

namespace BlueBoard.Module.Trip.Commands.Update
{
    public class UpdateTripCommandValidator : AbstractValidator<UpdateTripCommand>
    {
        public UpdateTripCommandValidator()
        {
            this.RuleFor(i => i.Trip)
                .NotNull().WithErrorCode(ErrorCodes.EmptyTrip);

            this.RuleFor(i => i.Trip.Id)
                .NotEmpty().WithErrorCode(ErrorCodes.EmptyTripId);

            this.RuleFor(i => i.Trip.Name)
                .NotEmpty().WithErrorCode(ErrorCodes.EmptyName)
                .SetValidator(new MaximumLengthValidator(Constraints.NameLength))
                .WithErrorCode(ErrorCodes.InvalidNameLength)
                .When(i => !string.IsNullOrEmpty(i.Trip.Name));

            this.RuleFor(i => i.Trip.Description)
                .SetValidator(new MaximumLengthValidator(Constraints.DescriptionLength))
                .WithErrorCode(ErrorCodes.InvalidDescriptionLength)
                .When(i => !string.IsNullOrEmpty(i.Trip.Description));

            this.RuleFor(i => i.Trip.StartDate)
                .GreaterThan(DateTime.UtcNow.Date).WithErrorCode(ErrorCodes.InvalidStartDate)
                .When(i => i.Trip.StartDate != default);

            this.RuleFor(i => i.Trip.EndDate)
                .GreaterThan(command => command.Trip.StartDate).WithErrorCode(ErrorCodes.InvalidEndDate)
                .When(i => i.Trip.StartDate != default && i.Trip.EndDate != default)
                .GreaterThan(DateTime.UtcNow.Date).WithErrorCode(ErrorCodes.InvalidEndDate)
                .When(i => i.Trip.EndDate != default);
        }
    }
}
