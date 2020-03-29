using System;
using System.Linq.Expressions;
using BlueBoard.Contract.Common;
using FluentValidation;
using FluentValidation.Validators;

namespace BlueBoard.Module.Common.Validation
{
    public static class ValidationExtensions
    {
        public static void ValidateEmail<T, TProperty>(this AbstractValidator<T> validator,
            Expression<Func<T, TProperty>> expression, Func<T, bool> when = null)
        {
            var builder = validator.RuleFor(expression)
                .NotEmpty().WithErrorCode(ErrorCodes.EmptyEmail)
                .SetValidator(new EmailValidator()).WithErrorCode(ErrorCodes.InvalidEmail);

            if (when != null) builder.When(when);
        }

        public static void ValidatePassword<T, TProperty>(this AbstractValidator<T> validator,
            Expression<Func<T, TProperty>> expression, Func<T, bool> when = null)
        {
            var builder = validator.RuleFor(expression)
                .NotEmpty().WithErrorCode(ErrorCodes.EmptyPassword)
                .SetValidator(new MinimumLengthValidator(6)).WithErrorCode(ErrorCodes.InvalidPasswordLength);

            if (when != null) builder.When(when);
        }

        public static void ValidatePhone<T, TProperty>(this AbstractValidator<T> validator,
            Expression<Func<T, TProperty>> expression, Func<T, bool> when)
        {
            var builder = validator.RuleFor(expression)
                .SetValidator(new PhoneValidator()).WithErrorCode(ErrorCodes.InvalidPhone);

            if (when != null) builder.When(when);
        }
    }
}
