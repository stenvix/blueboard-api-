using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BlueBoard.Module.Common.Exceptions;
using Dawn;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace BlueBoard.Module.Common
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : class
    {
        private readonly IEnumerable<IValidator<TRequest>> validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            this.validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            Guard.Argument(request, nameof(request)).NotNull();
            Guard.Argument(next, nameof(next)).NotNull();

            var context = new ValidationContext(request);

            var failures = this.validators
                .Select(i => i.Validate(context))
                .SelectMany(i => i.Errors)
                .Where(i => i != null)
                .ToList();

            if (failures.Count > 0)
            {
                throw new BlueBoardValidationException(this.GetErrors(failures));
            }

            return next();
        }

        private IDictionary<string, string[]> GetErrors(IList<ValidationFailure> failures)
        {
            var errors = new Dictionary<string, string[]>();
            var properties = failures.Select(i => i.PropertyName).Distinct().ToList();

            foreach (var property in properties)
            {
                var propertyFailures = failures
                    .Where(i => i.PropertyName == property)
                    .Select(i => i.ErrorCode)
                    .ToArray();

                errors.Add(property, propertyFailures);
            }

            return errors;
        }
    }
}
