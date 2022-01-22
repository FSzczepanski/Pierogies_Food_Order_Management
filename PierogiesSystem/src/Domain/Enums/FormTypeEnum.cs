namespace CleanArchitecture.Domain.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum FormTypeEnum
    {
        [Display(Description = "Wydarzenie")]
        Event = 1,
        [Display(Description = "Na miejscu")]
        ForHere = 2,
        [Display(Description = "Dostawa")]
        Delivery = 3
    }
}