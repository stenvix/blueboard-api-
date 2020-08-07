using BlueBoard.Contract.Common;
using BlueBoard.Contract.Identity.Queries;
using BlueBoard.Module.Common;
using FluentValidation;
using FluentValidation.Validators;

namespace BlueBoard.Module.Identity.Validators
{
    public class SearchUsersValidator : AbstractValidator<SearchUsersQuery>
    {
        public SearchUsersValidator()
        {
            this.RuleFor(i => i.Query)
                .SetValidator(new MinimumLengthValidator(Constraints.MinSearchLength)).WithErrorCode(ErrorCodes.InvalidQueryLength)
                .SetValidator(new MaximumLengthValidator(Constraints.NameLength)).WithErrorCode(ErrorCodes.InvalidQueryLength);
        }
    }
}
