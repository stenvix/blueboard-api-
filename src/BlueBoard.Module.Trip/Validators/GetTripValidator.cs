using BlueBoard.Contract.Common;
using BlueBoard.Contract.Trip.Queries;
using FluentValidation;

namespace BlueBoard.Module.Trip.Validators
{
    public class GetTripValidator : AbstractValidator<GetTrip>
    {
        public GetTripValidator()
        {
            this.RuleFor(i => i.TripId)
                .NotNull().WithErrorCode(ErrorCodes.InvalidId);
        }
    }
}
