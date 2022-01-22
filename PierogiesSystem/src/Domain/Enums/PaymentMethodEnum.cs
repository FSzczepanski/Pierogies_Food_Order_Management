namespace CleanArchitecture.Domain.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum PaymentMethodEnum
    {
        [Display(Description = "Na miejscu")]
        OnPlace = 1,
        [Display(Description = "Przelewy24")]
        Przelewy24 = 2
    }
}