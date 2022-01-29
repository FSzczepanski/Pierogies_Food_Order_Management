namespace CleanArchitecture.Application.Orders.Commands
{
    using Common.Validators;
    using FluentValidation;

    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(command => command.PurchaserName)
                .NotNull().NotEmpty().MaximumLength(150);;
            
            RuleFor(command => command.Email)
                .SetValidator(new EmailValidator<CreateOrderCommand>());
            RuleFor(command => command.Phone)
                .SetValidator(new PhoneValidator<CreateOrderCommand>());
            
            
            RuleFor(command => command.LocationName)
                .NotNull().NotEmpty().MaximumLength(150);;
            RuleFor(command => command.LocationDescription)
                .MaximumLength(300);
            RuleFor(command => command.Street)
                .MaximumLength(150);;
            RuleFor(command => command.ZipCode)
                .MaximumLength(20);;
            RuleFor(command => command.CityName)
                .MaximumLength(50);;
            RuleFor(command => command.CountryName)
                .MaximumLength(50);;
                
                
            RuleFor(command => command.Description)
                .MaximumLength(200);
        }
    }
}