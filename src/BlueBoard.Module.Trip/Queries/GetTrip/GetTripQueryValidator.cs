using BlueBoard.Contract.Common;
using BlueBoard.Contract.Trip.Queries;
using FluentValidation;

namespace BlueBoard.Module.Trip.Queries
{
    public class GetTripQueryValidator : AbstractValidator<GetTripQuery>
    {
        public GetTripQueryValidator()
        {
            this.RuleFor(i => i.TripId)
                .NotNull().WithErrorCode(ErrorCodes.InvalidId);
        }
    }
}
