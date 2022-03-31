namespace CleanArchitecture.Application.SystemSettings.Commands
{
    using FluentValidation;

    public class SaveSystemSettingsCommandValidator : AbstractValidator<SaveSystemSettingsCommand>
    {
        public SaveSystemSettingsCommandValidator()
        {
            RuleFor(command => command.Name)
                .NotNull().NotEmpty().MaximumLength(100);
            RuleFor(command => command.Nip)
                .MaximumLength(50);
            RuleFor(command => command.PhoneNumber).
                MaximumLength(100);
            RuleFor(command => command.Description)
                .MaximumLength(500);

            RuleFor(command => command.MaxKmFromLocation)
                .GreaterThanOrEqualTo(0).LessThan(500);
            RuleFor(command => command.GlobalDeliveryPrice)
                .GreaterThanOrEqualTo(0).LessThan(500);

        }
    }
}