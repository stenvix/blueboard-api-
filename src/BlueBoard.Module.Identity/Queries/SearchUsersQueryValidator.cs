using BlueBoard.Contract.Common;
using BlueBoard.Contract.Identity.Queries;
using BlueBoard.Persistence.Abstractions;
using FluentValidation;
using FluentValidation.Validators;

namespace BlueBoard.Module.Identity.Queries
{
    public class SearchUsersQueryValidator : AbstractValidator<SearchUsersQuery>
    {
        public SearchUsersQueryValidator()
        {
            this.RuleFor(i => i.Query)
                .SetValidator(new MinimumLengthValidator(Constraints.MinSearchLength)).WithErrorCode(ErrorCodes.InvalidQueryLength)
                .SetValidator(new MaximumLengthValidator(Constraints.NameLength)).WithErrorCode(ErrorCodes.InvalidQueryLength);
        }
    }
}
