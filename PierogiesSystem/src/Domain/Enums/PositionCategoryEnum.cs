namespace CleanArchitecture.Domain.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum PositionCategoryEnum
    {
        [Display(Description = "Brak")]
        None = 0,
        [Display(Description = "Oferta specjalna")]
        Promotion = 1,
        [Display(Description = "Przystawka")]
        Appetizer = 2,
        [Display(Description = "Zupa")]
        Soup = 3,
        [Display(Description = "Danie główne")]
        MainCourse = 4,
        [Display(Description = "Wegetariańskie")]
        Vegetarian = 5,
        [Display(Description = "Napoje")]
        Drinks = 6,
        [Display(Description = "Wyroby")]
        SelfMade = 7,
        [Display(Description = "Desery")]
        Dessert = 8
    }
}