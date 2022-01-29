namespace CleanArchitecture.Application.Orders.Commands
{
    using Common.Validators;
    using FluentValidation;

    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(command => command.Id)
                .NotNull().NotEmpty();
            
            RuleFor(command => command.PurchaserName)
                .NotNull().NotEmpty().MaximumLength(150);;
            
            RuleFor(command => command.Email)
                .SetValidator(new EmailValidator<UpdateOrderCommand>());
            RuleFor(command => command.Phone)
                .SetValidator(new PhoneValidator<UpdateOrderCommand>());
            
            
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