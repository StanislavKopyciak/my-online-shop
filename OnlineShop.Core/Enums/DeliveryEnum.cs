using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Core.Enums
{
    public enum DeliveryEnum
    {
        [Display(Name = "З доставкою")]
        WithDelivery = 1,
        [Display(Name = "Без доставки")]
        WithoutDelivery = 0
    }
}
