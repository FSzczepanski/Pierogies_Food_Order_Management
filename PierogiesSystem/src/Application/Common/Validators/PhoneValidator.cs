namespace CleanArchitecture.Application.Common.Validators
{
    using System.Text.RegularExpressions;
    using FluentValidation;
    using FluentValidation.Validators;
    using global::System;

    public class PhoneValidator<T> : PropertyValidator<T, string>
    {
        public override string Name => "PhoneValidator";

        public override bool IsValid(ValidationContext<T> context, string value)
        {
            try
            {
                var regex = new Regex(@"^\(?\+?[\d \s \( \) \-]+$");
                
                if (!regex.IsMatch(value))
                {
                    context.MessageFormatter.AppendArgument("EmailAddress", value);
                }
            }
            catch (Exception)
            {
                context.MessageFormatter.AppendArgument("PhoneNumber", value);
                return false;
            }

            return true;
        }
        
        protected override string GetDefaultMessageTemplate(string errorCode)
            => "Niepoprawny numer telefonu: {PhoneNumber}";
    }
}