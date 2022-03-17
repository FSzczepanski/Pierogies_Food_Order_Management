namespace CleanArchitecture.Application.Forms.Commands
{
    using FluentValidation;

    public class UpdateFormCommandValidator : AbstractValidator<UpdateFormCommand>
    {
        public UpdateFormCommandValidator()
        {
            RuleFor(command => command.Name)
                .NotNull().NotEmpty().MaximumLength(80);
            RuleFor(command => command.Description)
                .MaximumLength(400);
            RuleFor(command => command.FormType).IsInEnum();
            RuleFor(command => command.DeliveryPrice).GreaterThanOrEqualTo(0).LessThan(1000);
            RuleFor(command => command.MinimumTotalPrice).GreaterThanOrEqualTo(0).LessThan(10000);
        }
    }
}