using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Core.Enums
{
    public enum CategoryEnum
    {
        [Display(Name = "Електроніка")]
        Electronics = 1,
        [Display(Name = "Одежда")]
        Clothing = 2,
        [Display(Name = "Товари для дому")]
        HomeGoods = 3,
        [Display(Name = "Книги")]
        Books = 4,
        [Display(Name = "Косметика")]
        BeautyProducts = 5,
        [Display(Name = "Спортивний інвентар")]
        SportsEquipment = 6,
        [Display(Name = "Іграшки")]
        Toys = 7,
        [Display(Name = "Автомобілі")]
        Automotive = 8,
        [Display(Name = "Товари для здоров'я")]
        HealthProducts = 9,
        [Display(Name = "Зоотовари")]
        FoodAndBeverages = 10
    }
}