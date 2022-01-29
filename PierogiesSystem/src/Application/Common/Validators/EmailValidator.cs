namespace CleanArchitecture.Application.Common.Validators
{
    using FluentValidation;
    using FluentValidation.Validators;
    using global::System;
    using global::System.Text.RegularExpressions;

    public class EmailValidator<T> : PropertyValidator<T, string>
    {
        public override string Name => "EmailValidator";

        public override bool IsValid(ValidationContext<T> context, string value)
        {
            try
            {
                var regex = new Regex(
                    @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                if (!regex.IsMatch(value))
                {
                    context.MessageFormatter.AppendArgument("EmailAddress", value);
                }
            }
            catch (Exception)
            {
                context.MessageFormatter.AppendArgument("EmailAddress", value);
                return false;
            }

            return true;
        }

        protected override string GetDefaultMessageTemplate(string errorCode)
            => "Niepoprawny adres email: {EmailAddress}";
        
    }
}