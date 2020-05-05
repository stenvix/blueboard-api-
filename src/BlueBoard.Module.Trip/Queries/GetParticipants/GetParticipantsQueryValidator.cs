using BlueBoard.Contract.Common;
using BlueBoard.Contract.Trip.Queries;
using FluentValidation;

namespace BlueBoard.Module.Trip.Queries
{
    public class GetParticipantsQueryValidator: AbstractValidator<GetParticipantsQuery>
    {
        public GetParticipantsQueryValidator()
        {
            this.RuleFor(i => i.TripId)
                .NotEmpty().WithErrorCode(ErrorCodes.EmptyTripId);
        }
    }
}
