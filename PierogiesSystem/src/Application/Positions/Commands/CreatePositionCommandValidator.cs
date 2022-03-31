namespace CleanArchitecture.Application.Positions.Commands
{
    using FluentValidation;

    public class CreatePositionCommandValidator : AbstractValidator<CreatePositionCommand>
    {
        public CreatePositionCommandValidator()
        {
            RuleFor(command => command.Name)
                .NotNull().NotEmpty().MaximumLength(80);
            RuleFor(command => command.Description)
                .MaximumLength(200);

            RuleFor(command => command.Price)
                .NotNull().GreaterThan(0).LessThan(100000);
            RuleFor(command => command.Vat)
                .NotNull().GreaterThan(0).LessThan(1);
            RuleFor(command => command.Amount)
                .NotNull().GreaterThanOrEqualTo(0).LessThan(1000);
            RuleFor(command => command.PortionSize)
                .MaximumLength(200);
            
        }
        
    }
}