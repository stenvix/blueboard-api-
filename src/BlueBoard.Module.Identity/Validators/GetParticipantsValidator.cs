using BlueBoard.Contract.Common;
using BlueBoard.Contract.Identity.Queries;
using FluentValidation;

namespace BlueBoard.Module.Identity.Validators
{
    internal class GetParticipantsValidator: AbstractValidator<GetParticipants>
    {
        public GetParticipantsValidator()
        {
            this.RuleFor(i => i.TripId)
                .NotEmpty().WithErrorCode(ErrorCodes.EmptyTripId);
        }
    }
}
