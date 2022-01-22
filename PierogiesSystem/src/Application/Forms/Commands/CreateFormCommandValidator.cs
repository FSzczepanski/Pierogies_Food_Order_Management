namespace CleanArchitecture.Application.Forms.Commands
{
    using FluentValidation;

    public class CreateFormCommandValidator : AbstractValidator<CreateFormCommand>
    {
        public CreateFormCommandValidator()
        {
            RuleFor(command => command.Name)
                .NotNull().NotEmpty().MaximumLength(80);
            RuleFor(command => command.Description)
                .MaximumLength(400);
            RuleFor(command => command.FormType).IsInEnum();
            RuleFor(command => command.DeliveryPrice).GreaterThanOrEqualTo(0).LessThan(1000);
        }
    }
}