using FluentValidation.Validators;
using PhoneNumbers;

namespace BlueBoard.Module.Common.Validation
{
    public class PhoneValidator : PropertyValidator
    {
        private readonly PhoneNumberUtil util;

        public PhoneValidator() : base("{PropertyName} must be valid phone number.")
        {
            this.util = PhoneNumberUtil.GetInstance();
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var property = context.PropertyValue as string;
            try
            {
                var number = this.util.Parse(property, null);
                return this.util.IsValidNumber(number);
            }
            catch (NumberParseException e)
            {
                return false;
            }
        }
    }
}
